using UnityEngine;

/// <summary>
/// Sets the rigidbody X velocity each FixedUpdate,
/// allowing entities to walk given a desiredWalkDirection.
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
public class WalkMovement : MonoBehaviour
{
  /// <summary>
  /// Set by another component to request movement.
  /// Should be -1 (left) -> 1 (right)
  /// </summary>
  [Tooltip("Leave at 0 in the Inspector")]
  [Range(-1, 1)]
  public float desiredWalkDirection;

  [SerializeField]
  float walkSpeed = 100;

  Rigidbody2D myBody;

  protected void Awake()
  {
    Debug.Assert(desiredWalkDirection == 0,
      "desiredWalkDirection should be 0 in the Inspector");

    myBody = GetComponent<Rigidbody2D>();

    Debug.Assert(myBody != null);
  }

  protected void FixedUpdate()
  {
    float desiredXVelocity
      = desiredWalkDirection
        * walkSpeed
        * Time.fixedDeltaTime;

    // Set the X, even if we are not walking 
    // (to control horizontal momentum),
    // preserve the current Y (e.g. for gravity).
    myBody.velocity = new Vector2(
      desiredXVelocity,
      myBody.velocity.y);
  }
}