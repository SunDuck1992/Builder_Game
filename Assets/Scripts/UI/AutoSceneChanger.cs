using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoSceneChanger : MonoBehaviour
{
    [SerializeField] private EndLevel _endLevel;
    //[SerializeField] private ConstructionSite _constructionSite;

    private const string NeedChange = "yes";
    //private const string NoChange = "no";

    private void Awake()
    {
        if(PlayerPrefs.GetString("needChange") == NeedChange)
        {
            _endLevel.NextLevel();
        }
    }

}
