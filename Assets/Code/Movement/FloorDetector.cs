using UnityEngine;

/// <summary>
/// Collects information about the floor under this entity.
/// </summary>
[RequireComponent(typeof(Collider2D))]
public class FloorDetector : MonoBehaviour
{
  /// <summary>
  /// Bounds for the collider used to detect floors.
  /// This may be the entity collider or a specialized
  /// feet collider.
  /// </summary>
  public Bounds feetBounds
  {
    get
    {
      return myCollider.bounds;
    }
  }

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

  [SerializeField]
  ContactFilter2D floorFilter;

  static readonly Collider2D[] tempColliderList = new Collider2D[3];

  Collider2D myCollider;

  protected void Awake()
  {
    myCollider = GetComponent<Collider2D>();

    Debug.Assert(myCollider != null);
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
      Collider2D floorUnderUs = DetectFloorUnderUs();
      if(floorUnderUs != null)
      {
        distanceToFloor = CalculateDistanceToFloor(floorUnderUs);
      }
      else
      {
        distanceToFloor = null;
      }
    }
  }

  float CalculateDistanceToFloor(
    Collider2D floorUnderUs)
  {
    float yOfTopOfFloor = floorUnderUs.bounds.max.y;

    if(floorUnderUs is BoxCollider2D)
    {
      BoxCollider2D boxCollider = (BoxCollider2D)floorUnderUs;
      yOfTopOfFloor += boxCollider.edgeRadius;
    }

    return myCollider.bounds.min.y - yOfTopOfFloor;
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

  Collider2D DetectFloorUnderUs()
  {
    RaycastHit2D[] result = new RaycastHit2D[1];
    if(Physics2D.Raycast(
      transform.position,
      Vector2.down,
      floorFilter,
      result) > 0)
    {
      return result[0].collider;
    }

    return null;
  }

  Collider2D DetectTheFloorWeAreStandingOn()
  {
    int foundColliderCount
      = Physics2D.OverlapCollider(myCollider, floorFilter, tempColliderList);

    for(int i = 0; i < foundColliderCount; i++)
    {
      Collider2D collider = tempColliderList[i];
      ColliderDistance2D distance = collider.Distance(myCollider);

      if(distance.distance >= -.1f
        && Vector2.Dot(Vector2.up, distance.normal) > 0)
      {
        return collider;
      }
    }

    return null;
  }
}