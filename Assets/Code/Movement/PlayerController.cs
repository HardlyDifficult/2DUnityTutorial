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

  LadderMovement ladderMovement;

  protected void Awake()
  {
    walkMovement = GetComponent<WalkMovement>();
    jumpMovement = GetComponent<JumpMovement>();
    // InChildren as LadderMovement may be on the child Feet.
    ladderMovement = GetComponentInChildren<LadderMovement>();

    Debug.Assert(walkMovement != null);
    Debug.Assert(jumpMovement != null);
    Debug.Assert(ladderMovement != null);
  }

  protected void FixedUpdate()
  {
    walkMovement.desiredWalkDirection
      = Input.GetAxis("Horizontal");
    ladderMovement.desiredClimbDirection
      = Input.GetAxis("Vertical");
  }

  protected void Update()
  {
    if(Input.GetButtonDown("Jump"))
    {
      jumpMovement.jumpRequested = true;
    }
  }
}