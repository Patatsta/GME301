using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
           
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, float.PositiveInfinity))
            {
                Debug.DrawLine(Camera.main.transform.position, hit.point, Color.red, 1f); // Nur zur Visualisierung
               

                if (hit.collider.CompareTag("Enemy"))
                {
                   
                    MonsterBehaviour monsterBehaviour = hit.collider.GetComponent<MonsterBehaviour>();
                    if (monsterBehaviour != null)
                    {
                    
                        monsterBehaviour.Shot();
                    }
                }
            }
        }
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(Camera.main.transform.position, Camera.main.transform.position + Camera.main.transform.forward * 100f);
    }

}
