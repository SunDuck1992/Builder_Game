
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndLevel : MonoBehaviour
{
    [SerializeField] private string[] _sceneNames;
    [SerializeField] private BuildPoint _buildPoint;
    [SerializeField] private GameObject _root;
    [SerializeField] private TextMeshProUGUI _money;
    [SerializeField] private TextMeshProUGUI _score;

    private ConstructionSite _construction;

    private void Awake()
    {
        _buildPoint = FindObjectOfType<BuildPoint>();
        _buildPoint.OnBuild += Setup;
        _root.SetActive(false);
    }

    private void OnDestroy()
    {
        _buildPoint.OnBuild -= Setup;
    }

    public void NextLevel()
    {
        LeaderBoard.SetPlayer(UpgradePlayer.Instance.Score);
        Debug.Log("@Next!!");
        UpgradePlayer.Instance.StatisticMoney = 0;
        UpgradePlayer.Instance.StatisticScore = 0;
        PlayerPrefs.DeleteKey("s_money");
        PlayerPrefs.DeleteKey("s_score");
        PlayerPrefs.DeleteKey("house");
        PlayerPrefs.DeleteKey("houseNumber");
        Time.timeScale = 1f;
        var scene = _sceneNames[Random.Range(0, _sceneNames.Length)];
        Debug.Log("@Scene!!");
        SceneManager.LoadScene(scene);
        PlayerPrefs.SetString("scene_name", scene);
        Debug.Log("@End!!");
    }

    private void Setup(ConstructionSite constructionSite)
    {
        constructionSite.OnCompleteBuild += ShowPanel; 
        _construction = constructionSite;
    }

    private void ShowPanel()
    {
        _money.text = UpgradePlayer.Instance.StatisticMoney.ToString();
        _score.text = UpgradePlayer.Instance.StatisticScore.ToString();
        Time.timeScale = 0f;
        _root.SetActive(true);
    }
}
