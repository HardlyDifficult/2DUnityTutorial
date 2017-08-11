using System.Collections;
using UnityEngine;

/// <summary>
/// Fades alpha in for all of the sprites on this GameObject.
/// Once complete, an optional list of components are enabled.
/// </summary>
public class FadeInThenEnable : MonoBehaviour
{
  [SerializeField]
  float timeTillEnabled = 3;

  [SerializeField]
  Behaviour[] componentsToEnable;

  protected void OnEnable()
  {
    Debug.Assert(timeTillEnabled >= 0);

    StartCoroutine(FadeIn());
  }

  protected void OnDisable()
  {
    StopAllCoroutines();
  }

  IEnumerator FadeIn()
  {
    SpriteRenderer[] spriteList
      = gameObject.GetComponentsInChildren<SpriteRenderer>();
    Debug.Assert(spriteList.Length > 0);

    float timePassed = 0;
    while(timePassed < timeTillEnabled)
    {
      float percentComplete = timePassed / timeTillEnabled; 
      SetAlpha(spriteList, percentComplete * percentComplete);

      yield return null;

      timePassed += Time.deltaTime;
    }

    // Always end with alpha 1
    SetAlpha(spriteList, 1);

    for(int i = 0; i < componentsToEnable.Length; i++)
    {
      Behaviour component = componentsToEnable[i];
      component.enabled = true;
    }
  }

  void SetAlpha(
    SpriteRenderer[] spriteList,
    float alpha)
  {
    for(int i = 0; i < spriteList.Length; i++)
    {
      SpriteRenderer sprite = spriteList[i];

      // Set alpha, preserving the original color
      Color originalColor = sprite.color;
      sprite.color = new Color(
        originalColor.r,
        originalColor.g,
        originalColor.b,
        alpha);
    }
  }
}