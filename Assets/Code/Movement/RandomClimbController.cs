using System.Collections;
using UnityEngine;

/// <summary>
/// Uses the LadderMovement component to randomly climb
/// ladders up or down.
/// </summary>
[RequireComponent(typeof(LadderMovement))]
public class RandomClimbController : MonoBehaviour
{
  [SerializeField]
  float oddsOfClimbingLadderUp = .9f;

  [SerializeField]
  float oddsOfClimbingLadderDown = .1f;

  [SerializeField]
  float minTimeBetweenReconsideringDirection = 1;

  [SerializeField]
  float maxTimeBetweenReconsideringDirection = 10;

  LadderMovement ladderMovement;

  protected void Awake()
  {
    Debug.Assert(oddsOfClimbingLadderUp >= 0);
    Debug.Assert(oddsOfClimbingLadderDown >= 0);
    Debug.Assert(oddsOfClimbingLadderUp > 0 
      || oddsOfClimbingLadderDown > 0);
    Debug.Assert(minTimeBetweenReconsideringDirection >= 0);
    Debug.Assert(maxTimeBetweenReconsideringDirection > 0);
    Debug.Assert(minTimeBetweenReconsideringDirection
      < maxTimeBetweenReconsideringDirection);

    ladderMovement = GetComponent<LadderMovement>();

    Debug.Assert(ladderMovement != null);
  }

  protected void Start()
  {
    StartCoroutine(Wander());
  }

  IEnumerator Wander()
  {
    while(true)
    {
      SelectARandomClimbDirection();
      float timeToSleep = UnityEngine.Random.Range(
        minTimeBetweenReconsideringDirection,
        maxTimeBetweenReconsideringDirection);
      yield return new WaitForSeconds(timeToSleep);
    }
  }

  void SelectARandomClimbDirection()
  {
    if(ladderMovement.isOnLadder == false)
    {
      if(UnityEngine.Random.value <= oddsOfClimbingLadderUp)
      {
        ladderMovement.desiredClimbDirection = 1;
      }
      else if(UnityEngine.Random.value
        <= oddsOfClimbingLadderDown)
      {
        ladderMovement.desiredClimbDirection = -1;
      }
      else
      {
        ladderMovement.desiredClimbDirection = 0;
      }
    }
  }
}