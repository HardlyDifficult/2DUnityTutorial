using System;
using UnityEngine;

/// <summary>
/// Added to a weapon to manage pickup and equip.
/// </summary>
public class Weapon : MonoBehaviour
{
  Quaternion leftRotation, rightRotation;

  [SerializeField]
  Vector2 positionWhenEquip = new Vector2(.214f, .17f);

  [SerializeField]
  Vector3 rotationWhenEquipInEuler = new Vector3(0, 0, -90);

  [SerializeField]
  MonoBehaviour[] componentListToEnableOnEquip;

  WeaponHolder currentHolder;

  protected void Awake()
  {
    rightRotation = Quaternion.Euler(rotationWhenEquipInEuler);
    leftRotation = rightRotation * Quaternion.Euler(0, 0, 180);
  }

  protected void OnDestroy()
  {
    if(currentHolder != null)
    {
      currentHolder.turnAround.onTurnAround -= TurnAround_onTurnAround;
      currentHolder.currentWeapon = null;
    }
  }

  protected void OnTriggerEnter2D(
    Collider2D collision)
  {
    WeaponHolder holder = collision.GetComponent<WeaponHolder>();
    if(holder != null 
      && currentHolder == null 
      && holder.currentWeapon == null)
    {
      if(currentHolder != null)
      {
        currentHolder.turnAround.onTurnAround -= TurnAround_onTurnAround;
      }
      currentHolder = holder;
      currentHolder.turnAround.onTurnAround += TurnAround_onTurnAround;
      currentHolder.currentWeapon = gameObject;

      transform.SetParent(currentHolder.transform);
      RotateHammer();

      for(int i = 0; i < componentListToEnableOnEquip.Length; i++)
      {
        MonoBehaviour component = componentListToEnableOnEquip[i];
        component.enabled = true;
      }
    }
  }

  void TurnAround_onTurnAround()
  {
    RotateHammer();
  }

  void RotateHammer()
  {
    Quaternion targetRotation = currentHolder.turnAround.isFacingLeft
      ? leftRotation : rightRotation;
    transform.localRotation = targetRotation;
    Vector2 targetPosition = positionWhenEquip;
    if(currentHolder.turnAround.isFacingLeft)
    {
      targetPosition.x = -targetPosition.x;
    }
    transform.localPosition = targetPosition;
  }
}