using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    private static SpawnManager _instance;
    public static SpawnManager Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.Log("SpawnManager is NULL");
            }
            return _instance;
        }
    }

    [SerializeField] private Transform _monsterPrefab;
    [SerializeField] private Transform _spawnPosition;
    [SerializeField] private int _poolSize = 10;

    [SerializeField] private List<Transform> _monsterPool = new List<Transform>();

    private void Awake()
    {
        _instance = this;

        // Pool initialisieren
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
            yield return new WaitForSeconds(3f);
        }
    }

    private void SpawnMonster()
    {
        foreach (Transform monster in _monsterPool)
        {
            if (!monster.gameObject.activeInHierarchy)
            {
                print("Spawn");
                monster.position = _spawnPosition.position;
                monster.rotation = Quaternion.identity;
                monster.gameObject.SetActive(true);
                return;
            }
        }

    
        print("ADD");
        Transform monsterAddition = Instantiate(_monsterPrefab, Vector3.zero, Quaternion.identity);
        _monsterPool.Add(monsterAddition);
        monsterAddition.position = _spawnPosition.position;
        monsterAddition.rotation = Quaternion.identity;
        monsterAddition.gameObject.SetActive(true);
    }

}
