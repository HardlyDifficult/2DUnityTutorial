using UnityEngine;

/// <summary>
/// Flips a single sprite on this GameObject or its child
/// when the velocity's X is negative.
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
public class TurnAround : MonoBehaviour
{
  static readonly Quaternion flipRotation =
    Quaternion.Euler(0, 180, 0);

  Rigidbody2D myBody;

  bool isFacingLeft;

  protected void Awake()
  {
    myBody = GetComponent<Rigidbody2D>();

    Debug.Assert(myBody != null);
  }

  protected void FixedUpdate()
  {
    float xVelocity = myBody.velocity.x;

    // Don't flip if the entity is hardly moving
    if(Mathf.Abs(xVelocity) > 0.1)
    {
      bool isTravelingLeft = xVelocity < 0;
      if(isFacingLeft != isTravelingLeft)
      {
        isFacingLeft = isTravelingLeft;
        transform.rotation *= flipRotation;
      }
    }
  }
}