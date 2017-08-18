using System.Collections;
using UnityEngine;

/// <summary>
/// Flashes for a period of time when the entity dies.
/// The flash should accelerate as it gets closer to death.
/// </summary>
public class DeathEffectFlash : DeathEffect
{
  [SerializeField]
  float lengthToFlashFor = 5;

  [SerializeField]
  float timePerColorChange = .75f;

  [SerializeField]
  float colorChangeTimeFactorPerFlash = .85f;
  
  public override float PlayDeathEffects()
  {
    Debug.Assert(lengthToFlashFor > 0);
    Debug.Assert(timePerColorChange > 0);
    Debug.Assert(colorChangeTimeFactorPerFlash > 0);

    StartCoroutine(FlashToDeath());

    return lengthToFlashFor;
  }

  IEnumerator FlashToDeath()
  {
    SpriteRenderer[] spriteList
      = GetComponentsInChildren<SpriteRenderer>();
    Debug.Assert(spriteList.Length > 0);

    float timePassed = 0;
    bool isRed = false;
    while(timePassed < lengthToFlashFor)
    {
      SetColor(spriteList, isRed ? Color.red : Color.white);
      isRed = !isRed;

      yield return new WaitForSeconds(timePerColorChange);
      timePerColorChange = Mathf.Max(Time.deltaTime, timePerColorChange);
      timePassed += timePerColorChange;
      timePerColorChange *= colorChangeTimeFactorPerFlash;
    }
  }

  void SetColor(
    SpriteRenderer[] spriteList,
    Color color)
  {
    for(int i = 0; i < spriteList.Length; i++)
    {
      SpriteRenderer sprite = spriteList[i];
      sprite.color = color;
    }
  }
}