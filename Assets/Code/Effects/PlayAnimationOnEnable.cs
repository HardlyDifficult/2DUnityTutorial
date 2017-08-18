using UnityEngine;

/// <summary>
/// When this component is enabled, the selected animation
/// is played.
/// </summary>
public class PlayAnimationOnEnable : MonoBehaviour
{
  [SerializeField]
  string animationToPlay;

  Animator animator;

  protected void Awake()
  {
    animator = GetComponentInChildren<Animator>();
  }

  protected void OnEnable()
  {
    animator.Play(animationToPlay);
  }
}