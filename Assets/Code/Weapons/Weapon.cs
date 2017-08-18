using UnityEngine;

/// <summary>
/// Added to a weapon to manage pickup and equip.
/// </summary>
public class Weapon : MonoBehaviour
{
  Quaternion rotationWhenEquip;

  [SerializeField]
  Vector2 positionWhenEquip = new Vector2(.214f, .17f);

  [SerializeField]
  Vector3 rotationWhenEquipInEuler = new Vector3(0, 0, -90);

  [SerializeField]
  MonoBehaviour[] componentListToEnableOnEquip;

  WeaponHolder currentHolder;

  protected void Awake()
  {
    rotationWhenEquip = Quaternion.Euler(rotationWhenEquipInEuler);
  }

  protected void OnDestroy()
  {
    if(currentHolder != null)
    {
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
      currentHolder = holder;
      currentHolder.currentWeapon = gameObject;

      transform.SetParent(currentHolder.transform);
      transform.localRotation = rotationWhenEquip;
      transform.localPosition = positionWhenEquip;

      for(int i = 0; i < componentListToEnableOnEquip.Length; i++)
      {
        MonoBehaviour component = componentListToEnableOnEquip[i];
        component.enabled = true;
      }
    }
  }
}