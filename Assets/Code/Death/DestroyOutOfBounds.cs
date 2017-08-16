using UnityEngine;

/// <summary>
/// Destroy the GameObject anytime it falls off screen.
/// </summary>
public class DestroyOutOfBounds : MonoBehaviour
{
  /// <summary>
  /// 2 below the lowest point the camera can see.
  /// Hardcoded for simplicity.
  /// </summary>
  const float outOfBoundsYPosition = -12;

  protected void FixedUpdate()
  {
    if(transform.position.y < outOfBoundsYPosition)
    {
      Destroy(gameObject);
    }
  }
}