using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    [SerializeField] private LayerMask _targetMask; // Kombiniere _enemy und _barrier im Inspector oder per Code

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, float.PositiveInfinity, _targetMask))
            {
                Debug.DrawLine(Camera.main.transform.position, hit.point, Color.red, 1f);

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
