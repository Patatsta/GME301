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
    [SerializeField] private TMP_Text _hp;
    [SerializeField] private TMP_Text _score;

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
      
        _score.text = 0.ToString();
    }

    public void UpdateScore(int score)
    {
        _score.text = score.ToString();
    }

    public void UpdateHP(int hp)
    {
       
        _hp.text = "HP " + hp.ToString();
    }

    public void UpdateTimer(float time)
    {
        _timer.text = time.ToString("F1") + " s";
    }
}
