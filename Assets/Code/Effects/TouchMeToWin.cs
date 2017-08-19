using UnityEngine;

/// <summary>
/// This component will track how many touch regions there are
/// in the scene.  Once all have been touched, it triggers
/// the LevelController's YouWin method to end the level.
/// </summary>
public class TouchMeToWin : MonoBehaviour
{
  static int totalNumberActive;

  [SerializeField]
  Behaviour componentToEnableOnTouch;

  [SerializeField]
  LayerMask touchableLayers;
  
  protected void OnEnable()
  {
    Debug.Assert(touchableLayers.value != 0);

    totalNumberActive++;
  }

  protected void OnDisable()
  {
    totalNumberActive--;
  }

  protected void OnTriggerEnter2D(
    Collider2D collision)
  {
    if(enabled == false
      || touchableLayers.Includes(
        collision.gameObject.layer) == false)
    {
      return;
    }

    if(componentToEnableOnTouch != null)
    {
      componentToEnableOnTouch.enabled = true;
    }

    enabled = false;
    if(totalNumberActive == 0)
    {
      GameObject.FindObjectOfType<LevelController>().YouWin();
    }
  }
}