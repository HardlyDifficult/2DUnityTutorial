using System.Collections;
using UnityEngine;

/// <summary>
/// When the GameObject is spawned, this will set the 
/// rotation to 0.
/// Then when this component is enabled, this will rotate 
/// the GameObject back to its original rotation.  The
/// rotation is not smooth, creating an earthquake like
/// effect.
/// </summary>
public class RotateOvertimeToOriginal : MonoBehaviour
{
  [SerializeField]
  float rotationFactor = 1;

  [SerializeField]
  float maxTimeBetweenRotations = .25f;

  Quaternion targetRotation;

  protected void Awake()
  {
    targetRotation = transform.rotation;
    transform.rotation = Quaternion.identity;
  }

  protected void Start()
  {
    StartCoroutine(AnimateRotation());
  }

  IEnumerator AnimateRotation()
  {
    float percentComplete = 0;
    float sleepTimeLastFrame = 0;
    while(true)
    {
      sleepTimeLastFrame
        = UnityEngine.Random.Range(0, maxTimeBetweenRotations);
      yield return new WaitForSeconds(sleepTimeLastFrame);
      sleepTimeLastFrame = Mathf.Max(Time.deltaTime, sleepTimeLastFrame);

      float percentCompleteThisFrame = sleepTimeLastFrame * rotationFactor;
      percentCompleteThisFrame *= UnityEngine.Random.Range(0, 10);
      percentComplete += percentCompleteThisFrame;
      if(percentComplete >= 1)
      {
        transform.rotation = targetRotation;
        yield break;
      }
      transform.rotation = Quaternion.Lerp(
        Quaternion.identity,
        targetRotation,
        percentComplete);
    }
  }
}