using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MyAgent : MonoBehaviour
{
    public List<Transform> wayPoints = new List<Transform>();    
    NavMeshAgent agent;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        int random = Random.Range(0, wayPoints.Count);
        agent.SetDestination(wayPoints[random].position);
            
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
