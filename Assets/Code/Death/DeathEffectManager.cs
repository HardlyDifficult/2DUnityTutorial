using UnityEngine;

/// <summary>
/// Used to play effects before destroying a GameObject.
/// </summary>
public class DeathEffectManager : MonoBehaviour
{
  bool isInProcessOfDying;

  /// <summary>
  /// Call to Destroy a GameObject, possibly triggering
  /// multiple death effects and/or deferring the Destroy.
  /// </summary>
  public static void PlayDeathEffectsThenDestroy(
    GameObject gameObjectToDestroy)
  {
    DeathEffectManager deathEffectManager
      = gameObjectToDestroy.GetComponent<DeathEffectManager>();

    // If the GameObject does not have death effects, destroy now.
    if(deathEffectManager == null)
    {
      Destroy(gameObjectToDestroy);
      return;
    }

    deathEffectManager.PlayDeathEffectsThenDestroy();
  }

  void PlayDeathEffectsThenDestroy()
  {
    if(isInProcessOfDying)
    {
      return;
    }
    isInProcessOfDying = true;

    DeathEffect[] deathEffectList
      = gameObject.GetComponentsInChildren<DeathEffect>();

    float maxTimeTillDestroy = 0;
    for(int i = 0; i < deathEffectList.Length; i++)
    {
      DeathEffect deathEffect = deathEffectList[i];
      float timeTillDestroy = deathEffect.PlayDeathEffects();
      maxTimeTillDestroy = Mathf.Max(
        maxTimeTillDestroy,
        timeTillDestroy);
    }

    Destroy(gameObject, maxTimeTillDestroy);
  }
}