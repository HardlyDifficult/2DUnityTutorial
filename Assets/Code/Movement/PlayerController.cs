using UnityEngine;

/// <summary>
/// Reads input in order to drive movement.
/// </summary>
[RequireComponent(typeof(WalkMovement))]
public class PlayerController : MonoBehaviour
{
  WalkMovement walkMovement;

  protected void Awake()
  {
    walkMovement = GetComponent<WalkMovement>();

    Debug.Assert(walkMovement != null);
  }

  protected void FixedUpdate()
  {
    walkMovement.desiredWalkDirection
      = Input.GetAxis("Horizontal");
  }
}