using UnityEngine;

/// <summary>
/// Collects information about the floor under this entity.
/// </summary>
[RequireComponent(typeof(Collider2D))]
public class FloorDetector : MonoBehaviour
{
  public bool isTouchingFloor
  {
    get; private set;
  }

  public float? distanceToFloor
  {
    get; private set;
  }

  public Vector2? floorUp
  {
    get; private set;
  }

  public Quaternion? floorRotation
  {
    get; private set;
  }

  public Collider2D feetCollider
  {
    get; private set;
  }

  [SerializeField]
  ContactFilter2D floorFilter;

  /// <summary>
  /// Used for the OverlapCollider below.
  /// </summary>
  static readonly Collider2D[] tempColliderList = new Collider2D[3];

  /// <summary>
  /// Used for the Raycast below.
  /// </summary>
  static readonly RaycastHit2D[] tempHitList = new RaycastHit2D[1];

  protected void Awake()
  {
    feetCollider = GetComponent<Collider2D>();

    Debug.Assert(feetCollider != null);
  }

  protected void FixedUpdate()
  {
    Collider2D floorWeAreStandingOn = DetectTheFloorWeAreStandingOn();
    isTouchingFloor = floorWeAreStandingOn != null;

    if(floorWeAreStandingOn != null)
    {
      CalculateFloorRotation(floorWeAreStandingOn);
      distanceToFloor = 0;
    }
    else
    { // Not standing on a floor, find the floor under us
      floorUp = null;
      floorRotation = null;
      RaycastHit2D? floorUnderUs = DetectFloorUnderUs();
      if(floorUnderUs != null)
      {
        distanceToFloor = floorUnderUs.Value.distance;
      }
      else
      {
        distanceToFloor = null;
      }
    }
  }

  void CalculateFloorRotation(
    Collider2D floorWeAreStandingOn)
  {
    floorUp = floorWeAreStandingOn.transform.up;
    floorRotation = floorWeAreStandingOn.transform.rotation;
    if(Vector2.Dot(Vector2.up, floorUp.Value) < 0)
    {
      floorUp = -floorUp;
      floorRotation *= Quaternion.Euler(0, 0, 180);
    }
  }

  RaycastHit2D? DetectFloorUnderUs()
  {
    if(Physics2D.Raycast(
      transform.position,
      Vector2.down,
      floorFilter,
      tempHitList) > 0)
    {
      return tempHitList[0];
    }

    return null;
  }

  Collider2D DetectTheFloorWeAreStandingOn()
  {
    int foundColliderCount
      = Physics2D.OverlapCollider(
        feetCollider,
        floorFilter,
        tempColliderList);

    for(int i = 0; i < foundColliderCount; i++)
    {
      Collider2D collider = tempColliderList[i];
      ColliderDistance2D distance = collider.Distance(feetCollider);

      if(distance.distance >= -.1f
        && Vector2.Dot(Vector2.up, distance.normal) > 0)
      {
        return collider;
      }
    }

    return null;
  }
}