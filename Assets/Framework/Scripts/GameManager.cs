using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;
using UnityEngine.SceneManagement;
using GameDevHQ.FileBase.Plugins.FPS_Character_Controller;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    [SerializeField] private int _maxHP = 5;
    private int _currenthp;
    private int _score = 0;

    [SerializeField] float _startTime = 60f;
    private float _currentTime;
    [SerializeField] private GameObject _endScreen;
    [SerializeField] private TMP_Text _endTitel;
  
    [SerializeField] private FPS_Controller _controller;
    [SerializeField] private PlayerShooting _playershooting;

    private AudioSource _audioSource;
    [SerializeField] private AudioClip _vicSound;
    [SerializeField] private AudioClip _loseSound;
    [SerializeField] private AudioClip _damageClip;
    private bool _isGameOver = false;
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
        _audioSource = GetComponent<AudioSource>();
        _currenthp = _maxHP;
        _currentTime = _startTime;
        _endScreen.SetActive(false);
        
        UIManager.Instance.UpdateHP(_currenthp);
    }
    private void Update()
    {
        if (!_isGameOver)
        {
            if (Input.GetKeyUp(KeyCode.Escape))
            {
                SceneManager.LoadScene(0);
            }
            if (_currentTime > 0)
            {
            _currentTime -= Time.deltaTime;
            UIManager.Instance.UpdateTimer(Mathf.Round(_currentTime * 10f) / 10f);
             }
            else
            {
            
                _isGameOver=true;
                SpawnManager.Instance.GameEnd();
                EndGame(true);
             }
           
        }
    }

    public void AddScore(int points)
    {
        _score += points;
       
        UIManager.Instance.UpdateScore(_score);
    }

    public void PlayerDamaged()
    {
        _currenthp--;
        UIManager.Instance.UpdateHP(_currenthp);
        _audioSource.PlayOneShot(_damageClip);
        if (_currenthp <= 0)
        {
            _isGameOver = true;
            SpawnManager.Instance.GameEnd();
            EndGame(false);
        }
    }

    private void EndGame(bool isWin)
    {
        if (isWin)
        {
            _endTitel.text = "Victory";
            _endTitel.color = Color.green;
            _audioSource.PlayOneShot(_vicSound);
        }
        else
        {
            _endTitel.text = "Defeat";
            _endTitel.color = Color.red;
            _audioSource.PlayOneShot(_loseSound);
        }
      
        Cursor.lockState = CursorLockMode.None;
        _endScreen.SetActive(true);
        _playershooting.isGameOver = true;
        _controller.isGameOver = true;
        
        SoundManager.Instance.EndGame();
      
    }
    public void Restart()
    {
        SceneManager.LoadScene(1);
    }
    public void Quit()
    {
        SceneManager.LoadScene(0);
    }
}
