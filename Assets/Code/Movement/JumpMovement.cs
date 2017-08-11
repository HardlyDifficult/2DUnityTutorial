using UnityEngine;

/// <summary>
/// Adds a vertical impulse force when a jump is requested.
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(AudioSource))]
public class JumpMovement : MonoBehaviour
{
  /// <summary>
  /// Set by another component to request a jump.
  /// </summary>
  public bool jumpRequested;

  [SerializeField]
  AudioClip jumpSound;

  [SerializeField]
  float jumpSpeed = 7f;

  Rigidbody2D myBody;

  AudioSource audioSource;

  protected void Awake()
  {
    Debug.Assert(jumpSound != null);
    Debug.Assert(jumpSpeed > 0);

    myBody = GetComponent<Rigidbody2D>();
    audioSource = GetComponent<AudioSource>();

    Debug.Assert(myBody != null);
    Debug.Assert(audioSource != null);
  }

  protected void FixedUpdate()
  {
    if(jumpRequested)
    {
      myBody.AddForce(
          new Vector2(0, jumpSpeed),
          ForceMode2D.Impulse);

      audioSource.PlayOneShot(jumpSound);
    }

    jumpRequested = false;
  }
}