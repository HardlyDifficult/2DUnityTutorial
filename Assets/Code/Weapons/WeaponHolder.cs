using UnityEngine;

/// <summary>
/// This component allows an entity to hold a weapon.
/// </summary>
[RequireComponent(typeof(TurnAround))]
public class WeaponHolder : MonoBehaviour
{
  public TurnAround turnAround;

  public GameObject currentWeapon;

  protected void Awake()
  {
    turnAround = GetComponent<TurnAround>();

    Debug.Assert(turnAround != null);
  }
}