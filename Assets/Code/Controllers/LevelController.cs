using UnityEngine;
using UnityEngine.Playables;

/// <summary>
/// Coordinates level specific activities.
/// </summary>
public class LevelController : MonoBehaviour
{
  [SerializeField]
  GameObject characterPrefab;

  /// <summary>
  /// A reference to the director which owns the playable
  /// selected below.
  /// </summary>
  [SerializeField]
  PlayableDirector director;

  /// <summary>
  /// A reference to the playable timeline to use when the 
  /// player wins.
  /// </summary>
  [SerializeField]
  PlayableAsset youWinPlayable;

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

    BroadcastPlayerDied();

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

    director.Play(youWinPlayable);
  }

  void StartLevel()
  {
    Instantiate(characterPrefab);
  }

  void BroadcastPlayerDied()
  {
    PlayerDeathMonoBehaviour[] gameObjectList
      = GameObject.FindObjectsOfType<PlayerDeathMonoBehaviour>();
    for(int i = 0; i < gameObjectList.Length; i++)
    {
      PlayerDeathMonoBehaviour playerDeath = gameObjectList[i];
      playerDeath.OnPlayerDeath();
    }
  }

  void YouLose()
  {
    // TODO
    print("YouLose");
  }
}
