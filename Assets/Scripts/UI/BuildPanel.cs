using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;

public class BuildPanel : MonoBehaviour
{
    [SerializeField] private List<BuildProgress> _buildProgresses;
    [SerializeField] private TextMeshProUGUI _stageText;

    private ConstructionSite _construction;


    public void Setup(ConstructionSite constructionSite)
    {
        constructionSite.OnBuild += ShowProgress;
        constructionSite.OnComplete += OnCompleteStage;
        _construction = constructionSite;
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
        _stageText.text = _construction.House.CurrentStage.ToString();
        ShowMaterial();
    }

    private void ShowMaterial()
    {
        _buildProgresses.ForEach(x =>
        {
            if (_construction.House.StageMaterials.Contains(x.Materials))
            {
                x.gameObject.SetActive(true);
                x.ShowProgress(0, _construction.House.GetMaterialCount(x.Materials));
            }
            else
            {
                x.gameObject.SetActive(false);
            }
        });
    }
}
