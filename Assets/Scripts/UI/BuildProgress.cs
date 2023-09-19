using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BuildProgress : MonoBehaviour
{
    private const string Template = "{0} / {1}";
    [SerializeField] private TextMeshProUGUI _progressText;
    [SerializeField] private ConstructionSite _constructionSite;

    private void Start()
    {
        _constructionSite.OnBuild += ShowProgress;
    }

    private void OnDisable()
    {
        _constructionSite.OnBuild -= ShowProgress;
    }

    private void ShowProgress(int currentCount, int maxCount)
    {
        _progressText.text = string.Format(Template, currentCount, maxCount);
    }
}
