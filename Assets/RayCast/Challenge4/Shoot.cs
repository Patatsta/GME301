using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
  
    [SerializeField] private GameObject _bulletHole;
  

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;

            if(Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit))
            {
                if (hit.collider.CompareTag("Wall"))
                {
                    Instantiate(_bulletHole, hit.point, Quaternion.LookRotation(hit.normal));
                }
            }
        }
    }
}
