using UnityEngine;

/// <summary>
/// Extension methods for Unity's LayerMask.
/// </summary>
public static class LayerMaskExtensions
{
  /// <summary>
  /// Check if a layer is included in a given layer mask.
  /// </summary>
  public static bool Includes(
    this LayerMask mask,
    int layer)
  {
    return (mask.value & (1 << layer)) > 0;
  }
}