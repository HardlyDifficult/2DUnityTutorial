using UnityEngine;

/// <summary>
/// When death effects are triggered on this GameObject, this
/// spawns another GameObject.
/// </summary>
[RequireComponent(typeof(Collider2D))]
public class DeathEffectSpawn : DeathEffect
{
  [SerializeField]
  GameObject gameObjectToSpawnOnDeath;
  
  public override float PlayDeathEffects()
  {
    Debug.Assert(gameObjectToSpawnOnDeath != null);

    Collider2D collider = GetComponent<Collider2D>();
    Debug.Assert(collider != null);

    Instantiate(
      gameObjectToSpawnOnDeath,
      collider.bounds.center,
      Quaternion.identity);

    // GameObject may be destroyed now
    return 0;
  }
}