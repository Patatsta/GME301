using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class UIManager : MonoBehaviour
{
    private static UIManager instance;
    public static UIManager Instance
    {
        get 
        { 
            if (instance == null)
            {
                Debug.Log("UIManager is NULL");
            }
            return instance;
        }
    }

    [SerializeField] private TMP_Text _timer;
    [SerializeField] private TMP_Text _enemyCount;
    [SerializeField] private TMP_Text _score;

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        _enemyCount.text = "Enemies " + 0;
        _score.text = "Score " + 0;
    }

    public void UpdateScore(int score)
    {
        _score.text = "Score " + score;
    }

    public void UpdateEnemyCount(int enemy)
    {
        _enemyCount.text = "Enemies " + enemy;
    }

    public void UpdateTimer(float time)
    {
        _timer.text = time.ToString();
    }
}
