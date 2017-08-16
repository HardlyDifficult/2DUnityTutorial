using System;
using UnityEngine;

/// <summary>
/// Prevents a GameObject from leaving the visible area
/// of the world.
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
public class KeepOnScreen : MonoBehaviour
{
  Rigidbody2D myBody;

  public event Action onAttemptToLeaveScreen;

  protected void Awake()
  {
    myBody = GetComponent<Rigidbody2D>();

    Debug.Assert(myBody != null);
  }

  protected void FixedUpdate()
  {
    Bounds screenBounds = GameController.instance.screenBounds;
    if(screenBounds.Contains(transform.position) == false)
    {
      // Move to the closest on-screen location
      transform.position =
        screenBounds.ClosestPoint(transform.position);
      if(onAttemptToLeaveScreen != null)
      {
        onAttemptToLeaveScreen();
      }
    }
  }
}