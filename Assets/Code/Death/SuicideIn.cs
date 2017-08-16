using UnityEngine;
using System.Collections;

/// <summary>
/// Begins death effects and then destroys this GameObject
/// after timeTillDeath seconds.
/// </summary>
public class SuicideIn : MonoBehaviour
{
  [SerializeField]
  float timeTillDeath = 5;

  protected void Start()
  {
    Debug.Assert(timeTillDeath >= 0);

    StartCoroutine(CountdownToDeath());
  }

  IEnumerator CountdownToDeath()
  {
    yield return new WaitForSeconds(timeTillDeath);
    DeathEffectManager.PlayDeathEffectsThenDestroy(gameObject);
  }
}