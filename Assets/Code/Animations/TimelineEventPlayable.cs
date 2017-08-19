using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

/// <summary>
/// A playable to be used in a Timeline.  When initiated 
/// it will call every EnableComponentsOnTimelineEvent
/// in the scene with the select event type.
/// </summary>
public class TimelineEventPlayable : BasicPlayableBehaviour
{
  public enum EventType
  {
    AlmostAtStart, Start, End
  }

  /// <summary>
  /// The event type to broadcast for this instance 
  /// in the Timeline.
  /// </summary>
  [SerializeField]
  EventType eventType;

  public override void OnBehaviourPlay(
    Playable playable,
    FrameData info)
  {
    base.OnBehaviourPlay(playable, info);

    EnableComponentsOnTimelineEvent[] componentList
      = GameObject.FindObjectsOfType<EnableComponentsOnTimelineEvent>();

    for(int i = 0; i < componentList.Length; i++)
    {
      EnableComponentsOnTimelineEvent component = componentList[i];
      component.OnEvent(eventType);
    }
  }
}