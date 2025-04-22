using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private SoundManager _instance;
    public SoundManager Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.Log("Soundmanager is NULL");
            }
            return _instance;
        }
    }
    private void Awake()
    {
        _instance = this;
    }


}
