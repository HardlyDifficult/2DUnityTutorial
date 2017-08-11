using UnityEngine;
using System.Collections;

/// <summary>
/// When death effects are triggered on this GameObject, this
/// causes the GameObject to scale up and down until it's gone.
/// </summary>
public class DeathEffectThrob : DeathEffect
{
  [SerializeField]
  float lengthOfEffectInSeconds = 1;

  /// <summary>
  /// How many times to scale up and then down
  /// in the lengthOfEffectInSeconds.
  /// </summary>
  [SerializeField]
  int numberOfPulses = 5;

  Vector3 originalScale;

  protected void Awake()
  {
    Debug.Assert(lengthOfEffectInSeconds > 0);
    Debug.Assert(numberOfPulses > 0);

    originalScale = transform.localScale;

    Debug.Assert(originalScale.sqrMagnitude > 0);
  }

  public override float PlayDeathEffects()
  {
    StartCoroutine(ThrobToDeath());

    return lengthOfEffectInSeconds;
  }

  IEnumerator ThrobToDeath()
  {
    float timePerPulse
      = lengthOfEffectInSeconds / numberOfPulses;

    float timeRun = 0;
    while(timeRun < lengthOfEffectInSeconds)
    {
      float percentComplete
        = timeRun / lengthOfEffectInSeconds;
      float sinValue
        = Mathf.Sin(Mathf.PI * timeRun / timePerPulse);
      float pulse = .5f + Mathf.Abs(sinValue);
      float scale = (1 - percentComplete) * pulse;
      gameObject.transform.localScale
        = originalScale * scale; 

      yield return null;
      timeRun += Time.deltaTime;
    }
  }
}