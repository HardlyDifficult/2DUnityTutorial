using UnityEngine;

/// <summary>
/// Coordinates level specific activities.
/// </summary>
public class LevelController : MonoBehaviour
{
  [SerializeField]
  GameObject characterPrefab;

  bool isGameOver;
  
  protected void Start()
  {
    Debug.Assert(characterPrefab != null);
    Debug.Assert(isGameOver == false);

    GameController.instance.onLifeCountChange
      += Instance_onLifeCounterChange;

    StartLevel();
  }

  protected void OnDestroy()
  {
    GameController.instance.onLifeCountChange
      -= Instance_onLifeCounterChange;
  }

  void Instance_onLifeCounterChange()
  {
    if(isGameOver)
    {
      return;
    }

    if(GameController.instance.lifeCount <= 0)
    {
      isGameOver = true;
      YouLose();
    }
    else
    {
      StartLevel();
    }
  }

  public void YouWin()
  {
    if(isGameOver == true)
    {
      return;
    }
    isGameOver = true;

    // TODO
  }

  void StartLevel()
  {
    Instantiate(characterPrefab);
  }

  void YouLose()
  {
    // TODO
  }
}