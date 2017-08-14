using UnityEngine;

/// <summary>
/// Rotates an entity to match the rotation of the floor
/// they are standing on, if any.  Otherwise rotate back
/// to identity.
/// </summary>
public class RotateToAlignWithFloor : MonoBehaviour
{
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

  protected void Awake()
  {
    floorDetector
      = GetComponentInChildren<FloorDetector>();
  }

  protected void Update()
  {
    Quaternion rotation;
    float speed;
    if(floorDetector.floorRotation != null)
    {
      rotation = floorDetector.floorRotation.Value;
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