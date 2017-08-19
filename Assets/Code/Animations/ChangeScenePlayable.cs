using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
using UnityEngine.Timeline;

/// <summary>
/// This will load a scene when it first appears in a Timeline.
/// </summary>
public class ChangeScenePlayable : BasicPlayableBehaviour
{
  [SerializeField]
  string sceneNameToLoad;

  public override void OnBehaviourPlay(
    Playable playable,
    FrameData info)
  {
    Debug.Assert(string.IsNullOrEmpty(sceneNameToLoad) == false);

    base.OnBehaviourPlay(playable, info);

    SceneManager.LoadScene(sceneNameToLoad);
  }
}