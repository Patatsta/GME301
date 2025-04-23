using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StartScreenMonster : MonoBehaviour
{
    private Animator _anim;
    private NavMeshAgent _agent;

    [SerializeField] Transform _startPoint;
    [SerializeField] Transform _endPoint;
    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _anim = GetComponent<Animator>();
        transform.position = _startPoint.position;
        _agent.SetDestination(_endPoint.position);
    }

    // Update is called once per frame
    void Update()
    {
        _anim.SetFloat("Speed", _agent.velocity.magnitude);

        if (_agent.remainingDistance < 0.1f)
        {
            transform.position = _startPoint.position;
        }
    }
}
