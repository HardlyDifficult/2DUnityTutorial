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

  [SerializeField]
  ContactFilter2D contactFilter;

  Collider2D safeZoneCollider;

  static Collider2D[] tempColliderList = new Collider2D[1];

  protected void Awake()
  {
    safeZoneCollider = GetComponent<Collider2D>();
  }

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
      if(safeZoneCollider == null
        || safeZoneCollider.OverlapCollider(
          contactFilter, tempColliderList) == 0)
      {
        Instantiate(
        thingToSpawn,
        transform.position,
        Quaternion.identity);
      }

      // Sleep before the next spawn
      float sleepTime = UnityEngine.Random.Range(
        minTimeBetweenSpawns,
        maxTimeBetweenSpawns);
      yield return new WaitForSeconds(sleepTime);
    }
  }
}