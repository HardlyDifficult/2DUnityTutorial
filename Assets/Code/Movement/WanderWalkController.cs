using System.Collections;
using UnityEngine;

/// <summary>
/// Sets WalkMovement's desiredWalkDirection to 1 or -1,
/// switching directions periodically.
/// </summary>
[RequireComponent(typeof(WalkMovement))]
public class WanderWalkController : MonoBehaviour
{
  [SerializeField]
  float oddsOfGoingUpHill = .8f;

  [SerializeField]
  float timeBeforeFirstWander = 10;

  [SerializeField]
  float minTimeBetweenReconsideringDirection = 1;

  [SerializeField]
  float maxTimeBetweenReconsideringDirection = 10;

  WalkMovement walkMovement;

  FloorDetector floorDetector;

  protected void Awake()
  {
    Debug.Assert(timeBeforeFirstWander >= 0);
    Debug.Assert(minTimeBetweenReconsideringDirection >= 0);
    Debug.Assert(maxTimeBetweenReconsideringDirection 
      >= minTimeBetweenReconsideringDirection);
    Debug.Assert(maxTimeBetweenReconsideringDirection > 0);

    walkMovement = GetComponent<WalkMovement>();
    floorDetector = GetComponentInChildren<FloorDetector>();

    Debug.Assert(walkMovement != null);
    Debug.Assert(floorDetector != null);
  }

  protected void Start()
  {
    StartCoroutine(Wander());
  }

  IEnumerator Wander()
  {
    // Always start by walking right.
    walkMovement.desiredWalkDirection = 1;

    // Don't Wait if 0 as that will wait 1 frame.
    if(timeBeforeFirstWander > 0)
    {
      float timeToSleep = timeBeforeFirstWander + GetRandomTimeToSleep();
      yield return new WaitForSeconds(timeToSleep);
    }

    while(true)
    {
      SelectARandomWalkDirection();
      float timeToSleep = GetRandomTimeToSleep();
      yield return new WaitForSeconds(timeToSleep);
    }
  }

  void SelectARandomWalkDirection()
  {
    float dot;
    if(floorDetector.floorUp != null)
    {
      dot = Vector2.Dot(floorDetector.floorUp.Value, Vector2.right);
    }
    else
    {
      dot = 0;
    }

    if(dot < 0)
    {
      walkMovement.desiredWalkDirection
        = UnityEngine.Random.value <= oddsOfGoingUpHill ? 1 : -1;
    }
    else if(dot > 0)
    {
      walkMovement.desiredWalkDirection
        = UnityEngine.Random.value <= oddsOfGoingUpHill ? -1 : 1;
    }
    else
    {
      walkMovement.desiredWalkDirection
      = UnityEngine.Random.value <= .5f ? 1 : -1;
    }
  }

  float GetRandomTimeToSleep()
  {
    return UnityEngine.Random.Range(
      minTimeBetweenReconsideringDirection,
      maxTimeBetweenReconsideringDirection);
  }
}