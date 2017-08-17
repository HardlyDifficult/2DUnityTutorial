using UnityEngine;

/// <summary>
/// Called by the LevelController when the player dies.
/// </summary>
public abstract class PlayerDeathMonoBehaviour : MonoBehaviour
{
  public abstract void OnPlayerDeath();
}