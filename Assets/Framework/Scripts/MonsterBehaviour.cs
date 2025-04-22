using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class MonsterBehaviour : MonoBehaviour
{
    private NavMeshAgent _agent;
    private bool _isHiding = false;
    private bool _justActivated = false;
    private int _destinationIndex = -1;
    [SerializeField] private int _points = 50;

    private enum _monsterStates
    {
        Running,
        Hiding,
        Death
    }
    private _monsterStates _currentState;

    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
    }

    private void OnEnable()
    {
        _justActivated = true;
        _agent = GetComponent<NavMeshAgent>(); 
        _agent.isStopped = false;
        _destinationIndex = -1;
        _currentState = _monsterStates.Running;
        SetDestination();
    }

    private void Update()
    {
        if (_justActivated)
        {
            _justActivated = false;
            return; 
        }

        switch (_currentState)
        {
            case _monsterStates.Running:
                if (_agent.remainingDistance < 0.2f && !_agent.pathPending)
                {
                    
                    _isHiding = true;
                    _agent.isStopped = true;
                    _currentState = _monsterStates.Hiding;
                }
                break;

            case _monsterStates.Hiding:
                if (_isHiding)
                {
                    _isHiding = false;
                    StartCoroutine(HidingRoutine());
                }
                break;

            case _monsterStates.Death:
                break;
        }
    }

    private void SetDestination()
    {
        _currentState = _monsterStates.Running;
        _destinationIndex++;
        Transform nextPoint = SpawnManager.Instance.RequestNextPoint(_destinationIndex);
        if (nextPoint != null)
        {
            _agent.SetDestination(nextPoint.position);
        }
    }

    IEnumerator HidingRoutine()
    {
     
        float randomWait = Random.Range(2, 4);
        yield return new WaitForSeconds(randomWait);
        _agent.isStopped = false;
        SetDestination();
    }

    public void Shot()
    {
        _agent.isStopped = true;
        _currentState = _monsterStates.Death;
        GameManager.Instance.AddScore(_points);
        Invoke(nameof(SetInactive), 0.5f);
    }

    private void SetInactive()
    {
        gameObject.SetActive(false);
    }
}
