using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowCollidersIslandsEditor : MonoBehaviour
{
    private float radius;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnDrawGizmos()
    {
        radius = gameObject.GetComponent<CapsuleCollider>().radius;
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, radius);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
