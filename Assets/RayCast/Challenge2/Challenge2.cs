using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Challenge2 : MonoBehaviour
{
    [SerializeField] private GameObject _spherePrefab;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            print("1");
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                print("2");
                if (hit.collider.CompareTag("Floor"))
                {
                    print("3");
                    Instantiate(_spherePrefab, hit.point, Quaternion.identity);
                }
            } 
        }
    }
}
