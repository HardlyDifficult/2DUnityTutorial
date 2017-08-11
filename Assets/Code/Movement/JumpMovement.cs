using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(AudioSource))]
public class JumpMovement : MonoBehaviour
{
  public bool jumpRequested;

  [SerializeField]
  AudioClip jumpSound;

  [SerializeField]
  float jumpSpeed = 7f;

  Rigidbody2D myBody;

  AudioSource audioSource;

  protected void Awake()
  {
    myBody = GetComponent<Rigidbody2D>();
    audioSource = GetComponent<AudioSource>();
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