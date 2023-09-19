using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Localization : MonoBehaviour
{
    public TextAsset text;

    private void Start()
    {
        LocalizationData localization = new LocalizationData();

        localization.localKeys.Add(new LocalKeyValue() { Key = "ключ", Value = "значение" });
        localization.localKeys.Add(new LocalKeyValue() { Key = "ключ - 1", Value = "значение2" });

        string json = JsonUtility.ToJson(localization);
        File.WriteAllText(Application.streamingAssetsPath + "/Localization.json", json);
    }
}

[Serializable]
public class LocalizationData
{
    public List<LocalKeyValue> localKeys = new List<LocalKeyValue>();
}

[Serializable]
public class LocalKeyValue
{
    public string Key;
    public string Value;
}
