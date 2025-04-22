using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MyAgent2 : MonoBehaviour
{
    public List<Transform> wayPoints = new List<Transform>();
    private int _pointIndex = 0;
    NavMeshAgent agent;
    private bool _isReverse = false;
    private bool _isAttacking = false;  
    private enum States
    {
        Walking,
        Jumping,
        Attacking,
        Death
    }

    private States _currentState = States.Walking;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        int random = Random.Range(0, wayPoints.Count);
        agent.SetDestination(wayPoints[0].position);

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _currentState = States.Jumping;
            agent.isStopped = true;
        }
       

        switch (_currentState)
        {
            case States.Walking:
                CalculateMovement();
                break;

            case States.Jumping:
               
                break;

            case States.Attacking:
                if (!_isAttacking)
                {
                    StartCoroutine(GoWalk());
                    _isAttacking = true;
                }
                  

                break;

            case States.Death:
                break;

        }
      
    }

    void CalculateMovement()
    {
        if (agent.remainingDistance <= 0.2f)
        {
            if (_isReverse)
            {
                Reverse();
            }
            else
            {
                Forward();
            }

            agent.SetDestination(wayPoints[_pointIndex].position);

            _currentState = States.Attacking;

        }
    }

    IEnumerator GoWalk()
    {
        agent.isStopped = true;
        yield return new WaitForSeconds(1);
        agent.isStopped = false;
        _currentState = States.Walking;
        _isAttacking = false;
    }
    void Forward()
    {
        if (_pointIndex == wayPoints.Count - 1)
        {
            _isReverse = true;
            _pointIndex--;
        }
        else
        {
            _pointIndex++;
        }
    }
    void Reverse()
    {
        if (_pointIndex == 0)
        {
            _isReverse = false;
            _pointIndex++;
        }
        else
        {
            _pointIndex--;
        }
    }
}
