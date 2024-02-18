using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoSceneChanger : MonoBehaviour
{
    [SerializeField] private EndLevel _endLevel;

    private const string NeedChange = "yes";

    private void Awake()
    {
        if(PlayerPrefs.GetString("needChange") == NeedChange)
        {
            _endLevel.NextLevel();
        }
    }

}
