using UnityEngine;

/// <summary>
/// When KeepOnScreen detects this GameObject went off screen,
/// this component will switch the WalkMovement direction.
/// </summary>
[RequireComponent(typeof(KeepOnScreen))]
[RequireComponent(typeof(WalkMovement))]
public class BounceOffScreenEdges : MonoBehaviour
{
  WalkMovement walkMovement;

  protected void Awake()
  {
    walkMovement = GetComponent<WalkMovement>();

    Debug.Assert(walkMovement != null);
  }

  protected void Start()
  {
    KeepOnScreen keepOnScreen = GetComponent<KeepOnScreen>();
    Debug.Assert(keepOnScreen != null);

    keepOnScreen.onAttemptToLeaveScreen
      += KeepOnScreen_onAttemptToLeaveScreen;
  }

  void KeepOnScreen_onAttemptToLeaveScreen()
  {
    // If on the right side, walk left. And vice versa.
    walkMovement.desiredWalkDirection = 
      transform.position.x > 0 ? -1 : 1;
  }
}