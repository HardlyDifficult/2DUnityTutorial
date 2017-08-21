using UnityEngine;

/// <summary>
/// When this component is enabled, constraints are removed
/// from the rigidbody and the platform effector, if any, 
/// is disabled.
/// </summary>
public class UnfreezeAndDisablePlatformers : MonoBehaviour
{
  protected void OnEnable()
  {
    Rigidbody2D myBody = GetComponent<Rigidbody2D>();
    myBody.constraints = RigidbodyConstraints2D.None;

    PlatformEffector2D effector
      = GetComponent<PlatformEffector2D>();
    if(effector != null)
    {
      effector.enabled = false;
    }
  }
}