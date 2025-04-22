using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    [SerializeField] private TMP_Text _scoreText;
    private int _score = 0;
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

   

    public void AddScore(int points)
    {
        _score += points;
        _scoreText.text = _score.ToString();
    }
 
}
