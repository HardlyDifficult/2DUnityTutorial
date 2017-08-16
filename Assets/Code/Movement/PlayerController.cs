using UnityEngine;

/// <summary>
/// Reads input in order to drive movement.
/// </summary>
[RequireComponent(typeof(WalkMovement))]
[RequireComponent(typeof(JumpMovement))]
public class PlayerController : MonoBehaviour
{
  WalkMovement walkMovement;

  JumpMovement jumpMovement;

  protected void Awake()
  {
    walkMovement = GetComponent<WalkMovement>();
    jumpMovement = GetComponent<JumpMovement>();

    Debug.Assert(walkMovement != null);
    Debug.Assert(jumpMovement != null);
  }

  protected void FixedUpdate()
  {
    walkMovement.desiredWalkDirection
      = Input.GetAxis("Horizontal");
  }

  protected void Update()
  {
    if(Input.GetButtonDown("Jump"))
    {
      jumpMovement.jumpRequested = true;
    }
  }
}