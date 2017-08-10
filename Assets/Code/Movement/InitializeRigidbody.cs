using UnityEngine;

/// <summary>
/// Gives the rigidbody initial momentum.
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
public class InitializeRigidbody : MonoBehaviour
{
  [SerializeField]
  Vector2 startingVelocity = new Vector2(3, 0);

  [SerializeField]
  float startingAngularVelocity = -500;

  protected void Start()
  {
    Rigidbody2D myBody = GetComponent<Rigidbody2D>();
    Debug.Assert(myBody != null);

    // Set initial momentum
    myBody.velocity = startingVelocity;
    myBody.angularVelocity = startingAngularVelocity;
  }
}