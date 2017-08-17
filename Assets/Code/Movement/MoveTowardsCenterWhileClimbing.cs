using UnityEngine;

/// <summary>
/// This will move the entity towards the center of a ladder
/// while climbing.
/// </summary>
[RequireComponent(typeof(LadderMovement))]
public class MoveTowardsCenterWhileClimbing : MonoBehaviour
{
  [SerializeField]
  float speed = 1f;

  LadderMovement ladderMovement;

  protected void Awake()
  {
    ladderMovement = GetComponent<LadderMovement>();
  }

  protected void FixedUpdate()
  {
    GameObject ladder = ladderMovement.ladderWeAreOn;
    if(ladder != null)
    {
      float targetX = ladder.transform.position.x;
      float myX = transform.position.x;
      float deltaX = targetX - myX;
      if(Mathf.Abs(deltaX) > 0.01)
      {
        Vector2 target = transform.position;
        target.x += deltaX;
        transform.position = Vector2.MoveTowards(
          transform.position,
          target,
          speed * Time.fixedDeltaTime);
      }
    }
  }
}