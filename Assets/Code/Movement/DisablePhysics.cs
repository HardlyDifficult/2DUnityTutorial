using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// When enabled, physics on this GameObject are disabled.
/// When disabled, physics on this GameObject are re-enabled.
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
public class DisablePhysics : MonoBehaviour
{
  Rigidbody2D myBody;

  readonly List<Collider2D> impactedColliderList 
    = new List<Collider2D>();

  protected void Awake()
  {
    myBody = GetComponent<Rigidbody2D>();

    // Create a list of the non-trigger colliders
    Collider2D[] colliderList = GetComponentsInChildren<Collider2D>();
    for(int i = 0; i < colliderList.Length; i++)
    {
      Collider2D collider = colliderList[i];
      if(collider.isTrigger == false)
      {
        impactedColliderList.Add(collider);
      }
    }
  }

  protected void OnEnable()
  {
    myBody.gravityScale = 0;
    for(int i = 0; i < impactedColliderList.Count; i++)
    {
      Collider2D collider = impactedColliderList[i];
      collider.isTrigger = true;
    }
  }

  protected void OnDisable()
  {
    myBody.gravityScale = 1;
    for(int i = 0; i < impactedColliderList.Count; i++)
    {
      Collider2D collider = impactedColliderList[i];
      collider.isTrigger = false;
    }
  }
}