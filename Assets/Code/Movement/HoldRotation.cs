using UnityEngine;

/// <summary>
/// Each FixedUpdate, set the rotation back to its original.
/// </summary>
public class HoldRotation : MonoBehaviour
{
  Quaternion originalRotation;

  protected void Awake()
  {
    originalRotation = transform.rotation;
  }

  protected void FixedUpdate()
  {
    transform.rotation = originalRotation;
  }
}