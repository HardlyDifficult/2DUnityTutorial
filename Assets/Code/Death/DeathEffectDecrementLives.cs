/// <summary>
/// When death effects are triggered on this GameObject, this
/// reduces the GameControllers lifeCount by one.
/// </summary>
public class DeathEffectDecrementLives : DeathEffect
{
  public override float PlayDeathEffects()
  {
    GameController.instance.lifeCount--;

    // GameObject may be destroyed now
    return 0;
  }
}