using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndLevel : MonoBehaviour
{
    [SerializeField] private Object[] _scenes;
    [SerializeField] private BuildPoint _buildPoint;
    [SerializeField] private GameObject _root;

    private void Awake()
    {
        _buildPoint.OnBuild += Setup;
        _root.SetActive(false);
    }

    private void OnDestroy()
    {
        _buildPoint.OnBuild -= Setup;
    }

    public void NextLevel()
    {
        Time.timeScale = 1f;
        var scene = _scenes[Random.Range(0, _scenes.Length)];
        SceneManager.LoadScene(scene.name);
    }

    private void Setup(ConstructionSite constructionSite)
    {
        constructionSite.OnCompleteBuild += ShowPanel; 
    }

    private void ShowPanel()
    {
        Time.timeScale = 0f;
        _root.SetActive(true);
    }
}
