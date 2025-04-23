using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    private static SpawnManager _instance;
    public static SpawnManager Instance
    {
        get
        {
            if (_instance == null)
                Debug.Log("SpawnManager is NULL");
            return _instance;
        }
    }

    [SerializeField] private Transform _monsterPrefab;
    [SerializeField] private Transform _spawnPosition;
    [SerializeField] private Transform _endPoint;
    [SerializeField] private int _poolSize = 10;
    [SerializeField] private List<Transform> _HidePoints = new List<Transform>();

    [SerializeField] private List<Transform> _monsterPool = new List<Transform>();

    private List<Transform> _activeEnemies = new List<Transform>();

    [SerializeField] private float _spawnCD = 3f;
    private void Awake()
    {
        _instance = this;

        for (int i = 0; i < _poolSize; i++)
        {
            Transform monster = Instantiate(_monsterPrefab, Vector3.zero, Quaternion.identity);
            monster.gameObject.SetActive(false);
            _monsterPool.Add(monster);
        }
    }

    private void Start()
    {
        StartCoroutine(SpawnRoutine());
    }

    private IEnumerator SpawnRoutine()
    {
        while (true)
        {
            SpawnMonster();
            yield return new WaitForSeconds(_spawnCD); 
        }
    }

    private void SpawnMonster()
    {
        foreach (Transform monster in _monsterPool)
        {
            if (!monster.gameObject.activeInHierarchy)
            {
                monster.position = _spawnPosition.position;
                monster.rotation = Quaternion.identity;
                monster.gameObject.SetActive(true);
                _activeEnemies.Add(monster);
             
                return;
            }
        }

       
        Transform monsterAddition = Instantiate(_monsterPrefab, Vector3.zero, Quaternion.identity);
        _monsterPool.Add(monsterAddition);
        monsterAddition.position = _spawnPosition.position;
        monsterAddition.rotation = Quaternion.identity;
        monsterAddition.gameObject.SetActive(true);
        _poolSize++;

        _activeEnemies.Add(monsterAddition);
      
    }

    public Transform RequestNextPoint(int index)
    {
        if (index >= _HidePoints.Count)
        {
            return _endPoint;
        }
        return _HidePoints[index];
    }

    public void GameEnd()
    {
        StopAllCoroutines();
        foreach (Transform monster in _activeEnemies)
        {
            monster.gameObject.SetActive(false);
        }
    }

    public void RemoveActiveEnemy(Transform enemy)
    {
        _activeEnemies.Remove(enemy);
    }
}

