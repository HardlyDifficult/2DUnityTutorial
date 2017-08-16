using UnityEngine;

/// <summary>
/// Flips a single sprite on this GameObject or its child
/// when the velocity's X is negative.
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
public class TurnAround : MonoBehaviour
{
  Rigidbody2D myBody;

  SpriteRenderer sprite;

  protected void Awake()
  {
    myBody = GetComponent<Rigidbody2D>();
    sprite = GetComponentInChildren<SpriteRenderer>();

    Debug.Assert(myBody != null);
    Debug.Assert(sprite != null);
  }

  protected void FixedUpdate()
  {
    float xVelocity = myBody.velocity.x;

    // Don't flip if the entity is hardly moving
    if(Mathf.Abs(xVelocity) > 0.1)
    {
      bool isGoingLeft = xVelocity < 0;
      sprite.flipX = isGoingLeft;
    }
  }
}