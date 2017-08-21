using UnityEngine;

/// <summary>
/// This will randomize the scale, color, and position
/// of a TextMesh.  To be used on the GG screen.
/// </summary>
public class RandomGG : MonoBehaviour
{
  [SerializeField]
  float minScale = .05f;

  [SerializeField]
  float maxScale = .25f;

  protected void OnEnable()
  {
    transform.localScale
      = Vector3.one * UnityEngine.Random.Range(minScale, maxScale);

    TextMesh text = GetComponent<TextMesh>();
    text.color = UnityEngine.Random.ColorHSV();

    Bounds screenBounds = GameController.instance.screenBounds;
    transform.position = new Vector3(
      UnityEngine.Random.Range(screenBounds.min.x, screenBounds.max.x),
      screenBounds.max.y + 10,
      0);
  }
}