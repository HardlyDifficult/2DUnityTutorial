using UnityEditor;
using UnityEditor.SceneManagement;

/// <summary>
/// An editor script which automatically saves every time you
/// hit play.
/// </summary>
[InitializeOnLoad]
public class AutoSave
{
  static AutoSave()
  {
    EditorApplication.playmodeStateChanged
      += OnPlaymodeStateChanged;
  }

  /// <summary>
  /// Called on enter/exit play mode and pause.
  /// </summary>
  static void OnPlaymodeStateChanged()
  {
    // Skip saving if in play mode, e.g. during pause
    if(EditorApplication.isPlaying == false)
    {
      EditorSceneManager.SaveOpenScenes();
    }
  }
}