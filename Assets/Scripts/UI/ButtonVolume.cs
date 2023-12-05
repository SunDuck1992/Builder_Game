using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonVolume : MonoBehaviour
{
    [SerializeField] private GameObject _offVolume;

    private bool _enabled;

    private void Start()
    {
        _enabled = PlayerPrefs.GetInt("volumeMusic", 1) == 1;
        Sound();
    }

    public void ChangeSound()
    {
        _enabled = !_enabled;
        Sound();
        PlayerPrefs.SetInt("volumeMusic", (_enabled == true) ? 1 : 0);
    }

    private void Sound()
    {
        AudioListener.volume = (_enabled == true) ? 1.0f : 0.0f;
        _offVolume.SetActive(!_enabled);
    }
}
