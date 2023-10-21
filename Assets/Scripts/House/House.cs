using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StageInfo
{
    private Dictionary<Materials, MaterialInfo> _infoes = new();
    private List<Materials> _stageMaterials = new();

    public IReadOnlyList<Materials> StageMaterials => _stageMaterials;

    public(int max, int current) GetCountInfo(Materials material)
    {
        var materialInfo = _infoes[material];
        return (materialInfo.maxCount, materialInfo.currentCount);
    }

    public void AddMaterial(BuildMaterial buildMaterial, bool isLoad)
    {
        if (!_infoes.TryGetValue(buildMaterial.Materials, out var info))
        {
            info = new();
            _infoes.Add(buildMaterial.Materials, info);
        }

        if (isLoad)
        {
            buildMaterial.GetComponent<MeshRenderer>().enabled = true;
            info.currentCount++;
        }
        else
        {
            if (!_stageMaterials.Contains(buildMaterial.Materials))
            {
                _stageMaterials.Add(buildMaterial.Materials);
            }

            info.materialCells.Enqueue(buildMaterial);
        }

        info.maxCount++;
    }

    public BuildMaterial GetMaterial(Materials material)
    {
        if (_infoes.TryGetValue(material, out var info))
        {
            var element = info.materialCells.Dequeue();
            info.currentCount++;

            if (info.materialCells.Count <= 0)
            {
                _stageMaterials.Remove(material);
            }

            return element;
        }

        return null;
    }

    private class MaterialInfo
    {
        public Queue<BuildMaterial> materialCells = new();
        public int currentCount;
        public int maxCount;
    }
}

public class House : MonoBehaviour
{
    //private Dictionary<Materials, Queue<BuildMaterial>> _cellMaterials = new();
    //private List<Dictionary<Materials, Queue<BuildMaterial>>> _stageCells = new();
    //private Dictionary<int, Dictionary<Materials, int>> _stageCount = new();
    //private Dictionary<int, Dictionary<Materials, int>> _stageCurrentCount = new();
    private List<StageInfo> _stages = new List<StageInfo>();
    //private Queue<BuildMaterial> _cellsBricks = new ();
    [SerializeField] private Transform _root;

    private HouseProgress _saveData;

    public bool IsCanBuild => CurrentStage < _stages.Count;
    //public int MaxCount => _stageCount[CurrentStage];
    //public int CurrentCount { get; private set; }
    public int CurrentStage { get; private set; }
    public IReadOnlyList<Materials> StageMaterials => _stages[CurrentStage].StageMaterials;



    private void Awake()
    {
        string json = PlayerPrefs.GetString("house", string.Empty);

        if (string.IsNullOrEmpty(json))
        {
            _saveData = new HouseProgress();
        }
        else
        {
            _saveData = JsonUtility.FromJson<HouseProgress>(json);
            CurrentStage = _saveData.currentStage;
        }

        for (int i = 0; i < _root.childCount; i++)
        {
            var child = _root.GetChild(i);

            var materials = child.GetComponentsInChildren<BuildMaterial>();

            if (materials.Length > 0)
            {
                var stageInfo = new StageInfo();

                for (int j = 0; j < materials.Length; j++)
                {
                    stageInfo.AddMaterial(materials[j], _saveData.ContainTo(materials[j]));
                }

                _stages.Add(stageInfo);
            }
        }
    }

    public(int max, int current) GetCountInfo(Materials material)
    {
        return _stages[CurrentStage].GetCountInfo(material);
    }

    public void NextStage()
    {
        CurrentStage++;
        _saveData.currentStage = CurrentStage;
        string json = JsonUtility.ToJson(_saveData);
        PlayerPrefs.SetString("house", json);
    }
    public void BuildElement(Transform target, float speed, Materials materials)
    {
        if (target == null)
        {
            return;
        }

        var stageInfo = _stages[CurrentStage];
        var element = stageInfo.GetMaterial(materials);
        element?.PutBrick(target, speed);

        _saveData.Save(element);
        string json = JsonUtility.ToJson(_saveData);
        PlayerPrefs.SetString("house", json);

        //if (_stages[CurrentStage].TryGetValue(materials, out var queue))
        //{
        //    var element = queue.Dequeue();


        //    CurrentCount++;

        //    _saveData.name.Add(element.transform.parent.name);
        //    string json = JsonUtility.ToJson(_saveData);
        //    PlayerPrefs.SetString("house", json);

        //    if (queue.Count <= 0)
        //    {
        //        _stageCells[CurrentStage].Remove(materials);
        //    }

        //    if (_stageCells[CurrentStage].Count <= 0)
        //    {
        //        CurrentStage++;
        //        CurrentCount = 0;
        //    }
        //}
    }

    //public int GetMaterialCount(Materials material)
    //{
    //    return _stageCount[CurrentStage][material];
    //}

    //public void BuildElement(Transform target, float speed)
    //{
    //    if(target == null)
    //    {
    //        return;
    //    }

    //    var brick = _cellsBricks.Dequeue();
    //    brick.PutBrick(target, speed);
    //}

    [ContextMenu("Hide")]
    public void HideBricks()
    {
        var buildMaterials = transform.GetComponentsInChildren<BuildMaterial>();

        for (int i = 0; i < buildMaterials.Length; i++)
        {
            buildMaterials[i].GetComponent<MeshRenderer>().enabled = false;
        }
    }

    [ContextMenu("Show")]
    public void ShowBricks()
    {
        var buildMaterials = transform.GetComponentsInChildren<BuildMaterial>();

        for (int i = 0; i < buildMaterials.Length; i++)
        {
            buildMaterials[i].GetComponent<MeshRenderer>().enabled = true;
        }
    }
}
