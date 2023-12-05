using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        if (PlayerPrefs.HasKey("scene_name"))
        {
            SceneManager.LoadScene(PlayerPrefs.GetString("scene_name"));
        }
        else
        {
            SceneManager.LoadScene(2);
        }
    }
}
