using UnityEngine;

/// <summary>
/// When the level is over, this component will disable
/// a list of other components.
/// </summary>
public class DisableComponentsOnEndOfLevel : MonoBehaviour
{
  [SerializeField]
  Component[] componentsToDisable;

  public void OnEndOfLevel()
  {
    for(int i = 0; i < componentsToDisable.Length; i++)
    {
      Component component = componentsToDisable[i];
      if(component is Rigidbody2D)
      { // Disable simulation on a rigidbody
        Rigidbody2D myBody = (Rigidbody2D)component;
        myBody.simulated = false;
      }
      else if(component is Behaviour)
      { // Disable the component if it's a Behaviour
        Behaviour behaviour = (Behaviour)component;
        behaviour.enabled = false;
        if(behaviour is MonoBehaviour)
        { // Also stop coroutines if a MonoBehaviour
          MonoBehaviour monoBehaviour = (MonoBehaviour)behaviour;
          monoBehaviour.StopAllCoroutines();
        }
      }
      else
      { // Destroy other component types
        Destroy(component);
      }
    }
  }
}