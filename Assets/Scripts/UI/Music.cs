using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour
{   
    public static Music Instance;

    [SerializeField] private AudioSource _audio;

    void Start()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject); 
    }  

    public void ChangeVolume(float volume)
    {
        _audio.volume = volume;
    }
}
