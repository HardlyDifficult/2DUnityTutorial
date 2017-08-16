using System;
using UnityEngine;

/// <summary>
/// A singleton / DontDestroyOnLoad used to persist information
/// across scenes, such as points.
/// </summary>
public class GameController : MonoBehaviour
{
  public static GameController instance;

  public event Action onLifeCountChange;

  [SerializeField]
  int _lifeCount = 3;
  public int lifeCount
  {
    get
    {
      return _lifeCount;
    }
    set
    {
      _lifeCount = value;
      if(onLifeCountChange != null)
      {
        onLifeCountChange();
      }
    }
  }

  public int points;

  public Bounds screenBounds
  {
    get; private set;
  }

  int originalLifeCount;

  protected void Awake()
  {
    Debug.Assert(lifeCount > 0);
    Debug.Assert(points == 0);

    // If there is already a GameController, destroy the extra.
    if(instance != null)
    {
      Destroy(gameObject);
      return;
    }

    instance = this;
    DontDestroyOnLoad(gameObject);

    originalLifeCount = lifeCount;

    // Ensure screenBounds has a value when the game begins
    CalcScreenSize();
  }

  protected void Update()
  {
    // Update each frame in case the camera changed: which 
    // should not happen except for during scene changes.
    CalcScreenSize();
  }

  void CalcScreenSize()
  {
    Vector2 screenSize = new Vector2(
          (float)Screen.width / Screen.height,
          1);
    screenSize *= Camera.main.orthographicSize * 2;
    screenBounds = new Bounds(
      (Vector2)Camera.main.transform.position,
      screenSize);
  }
}