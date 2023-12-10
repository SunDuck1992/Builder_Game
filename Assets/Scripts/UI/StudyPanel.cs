using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StudyPanel : MonoBehaviour
{
    private void Awake()
    {
        if (PlayerPrefs.HasKey("startHouse"))
        {
            gameObject.SetActive(false);
        }
    }
}
