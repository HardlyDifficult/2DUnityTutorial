using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// When any key is pressed, the given scene is loaded.
/// </summary>
public class AnyKeyToLoadScene : MonoBehaviour
{
  [SerializeField]
  string sceneName = "Menu";

  protected void Update()
  {
    Debug.Assert(string.IsNullOrEmpty(sceneName) == false);

    if(Input.anyKeyDown)
    {
      SceneManager.LoadScene(sceneName);
    }
  }
}