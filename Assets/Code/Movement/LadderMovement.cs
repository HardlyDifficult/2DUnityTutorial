using System;
using UnityEngine;

/// <summary>
/// Climbs a ladder up or down given desiredClimbDirection.
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
public class LadderMovement : MonoBehaviour
{
  /// <summary>
  /// Set by another component to get on a ladder and to
  /// climb up or down.
  /// </summary>
  public float desiredClimbDirection;

  public event Action onGettingOnLadder;

  public event Action onGettingOffLadder;

  public bool isOnLadder
  {
    get
    {
      return ladderWeAreOn != null;
    }
  }

  [SerializeField]
  float climbSpeed = 60;

  /// <summary>
  /// Set to Use Triggers, Use LayerMask, and LayerMask Ladder.
  /// </summary>
  [SerializeField]
  ContactFilter2D ladderFilter;

  Rigidbody2D myBody;

  FloorDetector floorDetector;

  /// <summary>
  /// Used for OverlapCollider below.
  /// </summary>
  static readonly Collider2D[] tempColliderList
    = new Collider2D[3];

  GameObject _ladderWeAreOn;

  public GameObject ladderWeAreOn
  {
    get
    {
      return _ladderWeAreOn;
    }
    private set
    {
      Debug.Assert(value == null 
        || value.layer == LayerMask.NameToLayer("Ladder"));

      if(ladderWeAreOn == value)
      {
        return;
      }

      _ladderWeAreOn = value;

      if(ladderWeAreOn != null)
      {
        OnGettingOnLadder();
      }
      else
      {
        OnGettingOffLadder();
      }
    }
  }

  protected void Awake()
  {
    Debug.Assert(desiredClimbDirection == 0,
      "desiredClimbDirection should be 0 in the Inspector.");
    Debug.Assert(climbSpeed > 0);
    Debug.Assert(ladderFilter.useTriggers);
    Debug.Assert(ladderFilter.useLayerMask);
    Debug.Assert(ladderFilter.layerMask
      == LayerMask.GetMask("Ladder"));

    myBody = GetComponent<Rigidbody2D>();
    floorDetector = GetComponentInChildren<FloorDetector>();

    Debug.Assert(myBody != null);
    Debug.Assert(floorDetector != null);
  }

  protected void FixedUpdate()
  {
    GameObject ladder = ladderWeAreOn;

    if(ladder == null)
    {
      ladder = FindClosestLadder();
      if(ladder == null)
      { // No ladders in the area
        return;
      }
    }

    Bounds ladderBounds
      = ladder.GetComponent<Collider2D>().bounds;
    Bounds entityBounds = floorDetector.feetCollider.bounds;

    if(isOnLadder == false)
    {
      TryGettingOnLadder(ladder, ladderBounds, entityBounds);
    }

    if(isOnLadder)
    {
      ConsiderGettingOffLadder(ladderBounds, entityBounds);

      if(isOnLadder)
      {
        ClimbLadder();
      }
    }
  }

  /// <summary>
  /// Called by another component to get off a ladder.
  /// </summary>
  public void GetOffLadder()
  {
    ladderWeAreOn = null;
  }

  void TryGettingOnLadder(
    GameObject ladder,
    Bounds ladderBounds,
    Bounds entityBounds)
  {
    if(Mathf.Abs(desiredClimbDirection) > 0.01
      && IsInBounds(ladderBounds, entityBounds)
      && (
        desiredClimbDirection > 0
          && entityBounds.min.y < ladderBounds.center.y
        || desiredClimbDirection < 0
          && entityBounds.min.y > ladderBounds.center.y))
    {
      ladderWeAreOn = ladder;
    }
  }

  void ClimbLadder()
  {
    myBody.velocity = new Vector2(myBody.velocity.x,
      desiredClimbDirection * climbSpeed * Time.fixedDeltaTime);
  }

  void ConsiderGettingOffLadder(
    Bounds ladderBounds,
    Bounds entityBounds)
  {
    float currentVerticalVelocity = myBody.velocity.y;
    if(IsInBounds(ladderBounds, entityBounds) == false)
    {
      GetOffLadder();
    }
    else if(floorDetector.distanceToFloor < .3f
      && floorDetector.distanceToFloor > .1f)
    { // Just above the floor, consider getting off
      if(currentVerticalVelocity > 0
          && entityBounds.min.y > ladderBounds.center.y)
      { // Going up and above center
        GetOffLadder();
      }
      else if(currentVerticalVelocity < 0
        && entityBounds.min.y < ladderBounds.center.y)
      { // Going down and below center
        GetOffLadder();
      }
    }
  }

  void OnGettingOnLadder()
  {
    if(onGettingOnLadder != null)
    {
      onGettingOnLadder();
    }
  }

  void OnGettingOffLadder()
  {
    desiredClimbDirection = 0;

    if(onGettingOffLadder != null)
    {
      onGettingOffLadder();
    }
  }

  bool IsInBounds(
    Bounds ladderBounds,
    Bounds entityBounds)
  {
    float entityCenterX = entityBounds.center.x;
    if(ladderBounds.min.x > entityCenterX
      || ladderBounds.max.x < entityCenterX)
    {
      return false;
    }

    float entityFeetY = entityBounds.min.y;
    if(ladderBounds.min.y > entityFeetY
      || ladderBounds.max.y < entityFeetY)
    {
      return false;
    }

    return true;
  }

  GameObject FindClosestLadder()
  {
    int resultCount
      = floorDetector.feetCollider.OverlapCollider(
        ladderFilter, tempColliderList);

    GameObject closestLadder = null;
    float distanceToClosestLadder = 0;
    for(int i = 0; i < resultCount; i++)
    {
      GameObject ladder = tempColliderList[i].gameObject;
      Vector2 delta
        = ladder.transform.position
          - transform.position;
      float distanceToLadder = delta.sqrMagnitude;
      if(closestLadder == null
       || distanceToLadder < distanceToClosestLadder)
      {
        closestLadder = ladder;
        distanceToClosestLadder = distanceToLadder;
      }
    }

    return closestLadder;
  }
}