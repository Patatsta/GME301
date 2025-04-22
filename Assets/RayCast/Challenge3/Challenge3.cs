using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Challenge3 : MonoBehaviour
{
    private Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawLine(transform.position, Vector3.down, Color.blue);
        RaycastHit hitInfo;
        if(Physics.Raycast(transform.position, Vector3.down, out hitInfo, 1f))
        {
            if (hitInfo.collider.CompareTag("Floor"))
            {
                rb.useGravity = false;
                rb.isKinematic = true;
            }
         
        }
    }
}
