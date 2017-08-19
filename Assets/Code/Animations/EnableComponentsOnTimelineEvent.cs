using UnityEngine;

/// <summary>
/// When this script is triggerd by the TimelineEventPlayable
/// a list of components will be enabled.
/// </summary>
public class EnableComponentsOnTimelineEvent : MonoBehaviour
{
  [SerializeField]
  TimelineEventPlayable.EventType eventType;

  [SerializeField]
  Behaviour[] componentListToEnable;

  public void OnEvent(
    TimelineEventPlayable.EventType currentEventType)
  {
    Debug.Assert(componentListToEnable.Length > 0);

    if(currentEventType == eventType)
    {
      for(int i = 0; i < componentListToEnable.Length; i++)
      {
        Behaviour component = componentListToEnable[i];
        component.enabled = true;
      }
    }
  }
}