using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
 
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 100.0f))
            {
                Renderer rend = hit.collider.GetComponent<Renderer>();

                if (hit.collider.CompareTag("Cube") && rend != null)
                {
                    rend = hit.collider.GetComponent<Renderer>();
                    rend.material.color = Color.red;
                }else if (hit.collider.CompareTag("Capsule") && rend != null)
                {
                    rend = hit.collider.GetComponent<Renderer>();
                    rend.material.color = Color.black;
                }
               
            }
        }
    }
}
