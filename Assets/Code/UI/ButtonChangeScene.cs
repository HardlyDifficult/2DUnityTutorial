using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Loads a scene when the OnClickLoadScene method is called.
/// </summary>
public class ButtonChangeScene : MonoBehaviour
{
  [SerializeField]
  string sceneName;

  public void OnClickLoadScene()
  {
    SceneManager.LoadScene(sceneName);
  }
}