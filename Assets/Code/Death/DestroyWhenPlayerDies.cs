/// <summary>
/// When the player dies, destroy this GameObject.
/// </summary>
public class DestroyWhenPlayerDies : PlayerDeathMonoBehaviour
{
  public override void OnPlayerDeath()
  {
    Destroy(gameObject);
  }
}