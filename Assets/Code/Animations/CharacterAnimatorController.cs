using UnityEngine;

/// <summary>
/// Each frame, reports various stats to the animation 
/// controller.
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(LadderMovement))]
[RequireComponent(typeof(WeaponHolder))]
public class CharacterAnimatorController : MonoBehaviour
{
  Animator animator;

  Rigidbody2D myBody;

  LadderMovement ladderMovement;

  FloorDetector floorDetector;

  WeaponHolder weaponHolder;

  protected void Awake()
  {
    animator = GetComponentInChildren<Animator>();
    myBody = GetComponent<Rigidbody2D>();
    ladderMovement = GetComponent<LadderMovement>();
    floorDetector = GetComponentInChildren<FloorDetector>();
    weaponHolder = GetComponent<WeaponHolder>();

    Debug.Assert(animator != null);
    Debug.Assert(myBody != null);
    Debug.Assert(ladderMovement != null);
    Debug.Assert(floorDetector != null);
    Debug.Assert(weaponHolder != null);
  }

  protected void LateUpdate() 
  {
    animator.SetFloat("Speed", myBody.velocity.magnitude);
    animator.SetBool("isTouchingFloor", 
      floorDetector.isTouchingFloor);
    animator.SetBool("isClimbing", ladderMovement.isOnLadder);
    animator.SetBool("hasWeapon", 
      weaponHolder.currentWeapon != null);

    print(ladderMovement.isOnLadder);
  }
}