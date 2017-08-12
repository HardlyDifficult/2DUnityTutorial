using UnityEngine;

/// <summary>
/// On trigger, if the character is directly above, award points.
/// </summary>
[RequireComponent(typeof(BoxCollider2D))]
public class AwardPointsOnJumpOver : MonoBehaviour
{
  [SerializeField]
  int pointsToAward = 100;

  [SerializeField]
  float cooldownTime = 3;

  BoxCollider2D myCollider;

  /// <summary>
  /// The Character as well as any obstacles that should
  /// prevent awarding points, such as the floor.
  /// Should not include Enemy.
  /// </summary>
  [SerializeField]
  ContactFilter2D obstacleContactFilter;

  /// <summary>
  /// The Character.
  /// </summary>
  [SerializeField]
  LayerMask playerLayerMask;

  static RaycastHit2D[] tempHitList = new RaycastHit2D[1];

  float lastPickupTime;

  protected void Awake()
  {
    Debug.Assert(pointsToAward > 0);
    Debug.Assert(cooldownTime >= 0);

    myCollider = GetComponent<BoxCollider2D>();

    Debug.Assert(myCollider != null);
  }

  protected void OnTriggerStay2D(
    Collider2D collision)
  {
    if(Time.timeSinceLevelLoad - lastPickupTime < cooldownTime)
    {
      // Too soon
      return;
    }

    // Check what is directly above us
    int count = Physics2D.Raycast(
      transform.parent.position,
      Vector2.up,
      obstacleContactFilter,
      tempHitList);

    if(count > 0
      && playerLayerMask.Includes(
        tempHitList[0].collider.gameObject.layer))
    {
      GameController.instance.points += pointsToAward;

      lastPickupTime = Time.timeSinceLevelLoad;
    }
  }
}