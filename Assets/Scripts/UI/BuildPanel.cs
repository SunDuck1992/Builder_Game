using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;

public class BuildPanel : MonoBehaviour
{
    [SerializeField] private List<BuildProgress> _buildProgresses;
    [SerializeField] private TextMeshProUGUI _stageText;
    [SerializeField] private BuildPoint _buildPoint;

    //private ConstructionSite _construction;

    private void Awake()
    {
        _buildPoint.OnBuild += Setup;
    }

    private void OnDestroy()
    {
        _buildPoint.OnBuild -= Setup;
    }
    public void Setup(ConstructionSite constructionSite)
    {
        constructionSite.OnBuild += ShowProgress;
        constructionSite.OnComplete += OnCompleteStage;
        //_construction = constructionSite;
        ShowMaterial();

    }

    private void ShowProgress(Materials material,int currentCount, int maxCount)
    {
        BuildProgress progress = _buildProgresses.Find(x => x.Materials == material);

        if(progress != null)
        {
            progress.ShowProgress(currentCount, maxCount);
        }
    }

    private void OnCompleteStage()
    {
        _stageText.text = _buildPoint.Construction.House.CurrentStage.ToString();
        ShowMaterial();
    }

    private void ShowMaterial()
    {
        _buildProgresses.ForEach(x =>
        {
            if (_buildPoint.Construction.House.StageMaterials.Contains(x.Materials))
            {
                var countInfo = _buildPoint.Construction.House.GetCountInfo(x.Materials);
                x.gameObject.SetActive(true);
                x.ShowProgress(countInfo.current, countInfo.max);
            }
            else
            {
                x.gameObject.SetActive(false);
            }
        });
    }
}
