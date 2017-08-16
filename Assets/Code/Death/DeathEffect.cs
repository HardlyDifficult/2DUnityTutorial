using UnityEngine;

/// <summary>
/// Base class for logic which is executed when an entity dies.
/// </summary>
[RequireComponent(typeof(DeathEffectManager))]
public abstract class DeathEffect : MonoBehaviour
{
  /// <summary>
  /// Do not call directly.  
  /// Use DeathEffectManager instead.
  /// </summary>
  /// <returns>
  /// How long before the GameObject may be destroyed.
  /// </returns>
  public abstract float PlayDeathEffects();
}