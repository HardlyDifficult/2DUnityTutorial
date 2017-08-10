using UnityEngine;

public class DestroyOutOfBounds : MonoBehaviour
{
  const float outOfBoundsYPosition = -12;

  protected void Update()
  {
    if(transform.position.y < outOfBoundsYPosition)
    {
      Destroy(gameObject);
    }
  }
}