using System.Collections;
using UnityEngine;

/// <summary>
/// Moves the camera up/down/left/right to create
/// a screen shake effect for a period of time.
/// </summary>
public class ScreenShake : MonoBehaviour
{
  [SerializeField]
  float timeToShakeFor = 1.5f;

  [SerializeField]
  float maxTimeBetweenShakes = .2f;

  [SerializeField]
  float shakeMagnitude = 1;

  protected void Start()
  {
    StartCoroutine(ShakeCamera());
  }

  IEnumerator ShakeCamera()
  {
    Camera camera = Camera.main;
    Vector3 startingPosition = camera.transform.position;

    float timePassed = 0;
    while(timePassed < timeToShakeFor)
    {
      float percentComplete = timePassed / timeToShakeFor;
      percentComplete *= 2;
      if(percentComplete > 1)
      {
        percentComplete = 2 - percentComplete;
      }
      Vector2 deltaPosition
        = UnityEngine.Random.insideUnitCircle 
          * shakeMagnitude * percentComplete;
      camera.transform.position 
        = startingPosition + (Vector3)deltaPosition;

      float maxTime 
        = maxTimeBetweenShakes * (1 - percentComplete);
      float sleepTime
        = UnityEngine.Random.Range(0, maxTime);
      yield return new WaitForSeconds(sleepTime);
      sleepTime = Mathf.Max(Time.deltaTime, sleepTime);
      timePassed += sleepTime;
    }

    camera.transform.position = startingPosition;
  }
}