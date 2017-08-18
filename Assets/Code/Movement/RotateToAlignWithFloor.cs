using UnityEngine;

/// <summary>
/// Rotates an entity to match the rotation of the floor
/// they are standing on, if any.  Otherwise rotate back
/// to identity.
/// </summary>
public class RotateToAlignWithFloor : MonoBehaviour
{
  static readonly Quaternion flipRotation =
    Quaternion.Euler(0, 180, 0);

  /// <summary>
  /// Rotation lerp speed while standing.
  /// </summary>
  [SerializeField]
  float lerpSpeedToFloor = .4f;

  /// <summary>
  /// Rotation lerp speed while in the air.
  /// </summary>
  [SerializeField]
  float lerpSpeedWhileInAir = .05f;

  FloorDetector floorDetector;

  TurnAround turnAround;

  protected void Awake()
  {
    floorDetector
      = GetComponentInChildren<FloorDetector>();
    turnAround = GetComponent<TurnAround>();

    Debug.Assert(floorDetector != null);
  }

  protected void Update()
  {
    Quaternion rotation;
    float speed;
    if(floorDetector.floorRotation != null)
    {
      rotation = floorDetector.floorRotation.Value;
      if(turnAround != null && turnAround.isFacingLeft)
      {
        rotation *= flipRotation;
      }
      speed = lerpSpeedToFloor;
    }
    else
    {
      rotation = Quaternion.identity;
      speed = lerpSpeedWhileInAir;
    }

    transform.rotation = Quaternion.Lerp(
      transform.rotation,
      rotation,
      speed * Time.deltaTime);
  }
}