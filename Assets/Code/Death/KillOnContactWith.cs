using UnityEngine;

/// <summary>
/// Trigger death effects and then destroy any GameObject
/// we come in contact with if it matches the LayerMask.
/// </summary>
[RequireComponent(typeof(Collider2D))]
public class KillOnContactWith : MonoBehaviour
{
  [SerializeField]
  LayerMask layersToKill;

  /// <summary>
  /// Added to allow enable/disable in the Inspector.
  /// </summary>
  protected void Start() { }

  protected void OnCollisionEnter2D(
    Collision2D collision)
  {
    TryToKill(collision.gameObject);
  }

  protected void OnTriggerEnter2D(
    Collider2D collision)
  {
    TryToKill(collision.gameObject);
  }

  void TryToKill(
    GameObject gameObjectWeHit) 
  { 
    if(enabled == false)
    {
      return;
    }
    
    if(layersToKill.Includes(gameObjectWeHit.layer))
    {
      DeathEffectManager.PlayDeathEffectsThenDestroy(
        gameObjectWeHit);
    }
  }
}