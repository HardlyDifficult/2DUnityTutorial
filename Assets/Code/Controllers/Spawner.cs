using System.Collections;
using UnityEngine;

/// <summary>
/// Periodically spawns a GameObject at this object's position.
/// </summary>
public class Spawner : MonoBehaviour
{
  [SerializeField]
  GameObject thingToSpawn;

  [SerializeField]
  float minTimeBetweenSpawns = .5f;

  [SerializeField]
  float maxTimeBetweenSpawns = 10;

  protected void Start()
  {
    Debug.Assert(thingToSpawn != null);
    Debug.Assert(minTimeBetweenSpawns >= 0);
    Debug.Assert(maxTimeBetweenSpawns > 0);
    Debug.Assert(maxTimeBetweenSpawns >= minTimeBetweenSpawns);

    StartCoroutine(SpawnEnemiesCoroutine());
  }

  IEnumerator SpawnEnemiesCoroutine()
  {
    while(true)
    {
      Instantiate(
        thingToSpawn,
        transform.position,
        Quaternion.identity);

      // Sleep before the next spawn
      float sleepTime = UnityEngine.Random.Range(
        minTimeBetweenSpawns,
        maxTimeBetweenSpawns);
      yield return new WaitForSeconds(sleepTime);
    }
  }
}