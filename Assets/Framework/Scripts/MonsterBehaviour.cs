using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterBehaviour : MonoBehaviour
{
    private NavMeshAgent _agent;
    private Transform _EndPosition;
    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _EndPosition = GameObject.Find("EndPosition").transform;
        _agent.SetDestination(_EndPosition.position);
    }

  
}
