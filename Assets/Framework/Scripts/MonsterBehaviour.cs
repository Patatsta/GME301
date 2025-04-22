using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterBehaviour : MonoBehaviour
{
    private NavMeshAgent _agent;
    [SerializeField] private Transform _EndPosition;
    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _agent.SetDestination(_EndPosition.position);
    }

  
    void Update()
    {
        
    }
}
