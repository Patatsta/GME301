using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
   
    private int _score = 0;

    [SerializeField] float _startTime = 60f;
    private float _currentTime;
    public static GameManager Instance
    {
        get
        {
            if(instance == null)
            {
                Debug.Log("GameManager is NULL");
            }
            return instance;
        }
    }
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        _currentTime = _startTime;
    }
    private void Update()
    {
        if(_currentTime > 0)
        {
            _currentTime -= Time.deltaTime;
            UIManager.Instance.UpdateTimer(Mathf.Round(_currentTime * 100f) / 100f);
        }
    }

    public void AddScore(int points)
    {
        _score += points;
       
        UIManager.Instance.UpdateScore(_score);
    }
 
}
