using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This will update the Text component with the current
/// number of points.  The points displayed will scroll 
/// up overtime to its final value.
/// </summary>
public class TextPoints : MonoBehaviour
{
  [SerializeField]
  float scrollSpeed = .1f;

  Text text;

  int lastPointsDisplayed = -1;

  protected void Awake()
  {
    Debug.Assert(scrollSpeed > 0);

    text = GetComponent<Text>();

    Debug.Assert(text != null);
  }

  protected void Update()
  {
    int currentPoints = GameController.instance.points;
    int deltaPoints = currentPoints - lastPointsDisplayed;
    if(deltaPoints > 0)
    {
      float speed = scrollSpeed * Time.deltaTime;
      float pointsTarget =
        Mathf.Lerp(lastPointsDisplayed, currentPoints, speed);
      int pointsToDisplay = (int)pointsTarget;
      if(pointsToDisplay == lastPointsDisplayed)
      {
        pointsToDisplay++;
      }
      text.text = pointsToDisplay.ToString("N0");
      lastPointsDisplayed = pointsToDisplay;
    }
  }
}