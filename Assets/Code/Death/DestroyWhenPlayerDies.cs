/// <summary>
/// When the player dies, this GameObject will be destroyed.
/// </summary>
public class DestroyWhenPlayerDies : PlayerDeathMonoBehaviour
{
  public override void OnPlayerDeath()
  {
    Destroy(gameObject);
  }
}