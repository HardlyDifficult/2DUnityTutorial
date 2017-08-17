using UnityEngine;

/// <summary>
/// When getting on a ladder, this stores the momentum
/// and stops it, allowing the entity to climb straight
/// up or down.  When getting off, restore the momentum.
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(LadderMovement))]
public class StopMomentumOnLadder : MonoBehaviour
{
  Rigidbody2D myBody;

  float previousAngularVelocity;

  float previousXVelocity;

  protected void Awake()
  {
    myBody = GetComponent<Rigidbody2D>();

    LadderMovement ladderMovement 
      = GetComponent<LadderMovement>();
    ladderMovement.onGettingOffLadder
      += ClimbLadder_onGettingOffLadder;
    ladderMovement.onGettingOnLadder
      += LadderMovement_onGettingOnLadder;

    Debug.Assert(myBody != null);
  }

  void LadderMovement_onGettingOnLadder()
  {
    previousAngularVelocity = myBody.angularVelocity;
    previousXVelocity = myBody.velocity.x;
    myBody.velocity = Vector2.zero;
  }

  void ClimbLadder_onGettingOffLadder()
  {
    myBody.angularVelocity = -previousAngularVelocity;
    myBody.velocity = new Vector2(
      -previousXVelocity, myBody.velocity.y);
  }
}