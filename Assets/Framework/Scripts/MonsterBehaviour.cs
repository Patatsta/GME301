using System.Collections;
using System.Threading;
using UnityEngine;
using UnityEngine.AI;

public class MonsterBehaviour : MonoBehaviour
{
    private NavMeshAgent _agent;
    private bool _isHiding = false;
    private bool _justActivated = false;
    private int _destinationIndex = -1;
    [SerializeField] private int _points = 50;
    private Animator _anim;
    private AudioSource _audioSource;
    private Collider _collider;
    private enum _monsterStates
    {
        Running,
        Hiding,
        Death
    }
    private _monsterStates _currentState;

    private void Awake()
    {
        _collider = GetComponent<Collider>();
        _audioSource = GetComponent<AudioSource>();
        _anim = GetComponent<Animator>();
        _agent = GetComponent<NavMeshAgent>();
    }

    void OnEnable()
    {
        _justActivated = true;
        _collider.enabled = true;
        _agent = GetComponent<NavMeshAgent>();
        _agent.isStopped = false;
        _destinationIndex = -1;
        _currentState = _monsterStates.Running;
        SetDestination();
    }


    private void Update()
    {
        _anim.SetFloat("Speed", _agent.velocity.magnitude);

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
        _agent.isStopped = false;
        
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
     
        SetDestination();
    }

    public void Shot()
    {
        _collider.enabled = false;
        _anim.SetBool("Death", true);
        _audioSource.Play();
        _agent.isStopped = true;
        _currentState = _monsterStates.Death;
        GameManager.Instance.AddScore(_points);
        Invoke(nameof(SetInactive), 2f);
    }

    private void SetInactive()
    {
        gameObject.SetActive(false);
        _destinationIndex = -1;
    }
}
