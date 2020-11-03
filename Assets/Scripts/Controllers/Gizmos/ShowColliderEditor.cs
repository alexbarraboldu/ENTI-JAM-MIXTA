using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowColliderEditor : MonoBehaviour
{
    private BoxCollider boxCollider;
    
    private void OnDrawGizmos()
    {
        boxCollider = gameObject.GetComponent<BoxCollider>();
        Gizmos.DrawWireCube(transform.position, boxCollider.size);
    }
    
}
