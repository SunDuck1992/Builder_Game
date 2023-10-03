using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class House : MonoBehaviour
{
    //private Dictionary<Materials, Queue<BuildMaterial>> _cellMaterials = new();
    private List<Dictionary<Materials, Queue<BuildMaterial>>> _stageCells = new();
    private Dictionary<int, Dictionary<Materials,int>> _stageCount = new();
    private Dictionary<int, Dictionary<Materials, int>> _stageCurrentCount = new();

    //private Queue<BuildMaterial> _cellsBricks = new ();
    [SerializeField] private Transform _root;

    private HouseProgress _saveData;

    public bool IsCanBuild => CurrentStage < _stageCells.Count;
    //public int MaxCount => _stageCount[CurrentStage];
    public int CurrentCount { get; private set; }
    public int CurrentStage { get; private set; }
    public IReadOnlyList<Materials> StageMaterials => _stageCells[CurrentStage].Keys.ToArray();



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
        }

        for (int i = 0; i < _root.childCount; i++)
        {
            var child = _root.GetChild(i);

            var materials = child.GetComponentsInChildren<BuildMaterial>();
            Dictionary<Materials, Queue<BuildMaterial>> stage = new();

            for (int j = 0; j < materials.Length; j++)
            {
                if(!_stageCount.TryGetValue(i, out var materialsCount))
                {
                    materialsCount = new();
                    _stageCount.Add(i, materialsCount);
                }

                materialsCount[materials[j].Materials]++;

                if (!_saveData.name.Contains(materials[j].transform.parent.name))
                {
                    if (!stage.TryGetValue(materials[j].Materials, out var queue))
                    {
                        queue = new Queue<BuildMaterial>();
                        stage.Add(materials[j].Materials, queue);
                    }

                    queue.Enqueue(materials[j]);
                }
                else
                {
                    materials[j].GetComponent<MeshRenderer>().enabled = true;
                    CurrentCount++;
                }

                
            }
            _stageCells.Add(stage);

            if (stage.Count <= 0)
            {
                CurrentStage++;
                CurrentCount = 0;
            }
        }
    }
    public bool CheckMaterial(Materials material)
    {
        return _stageCells[CurrentStage].ContainsKey(material);
    }

    public void BuildElement(Transform target, float speed, Materials materials)
    {
        if (target == null)
        {
            return;
        }

        if (_stageCells[CurrentStage].TryGetValue(materials, out var queue))
        {
            var element = queue.Dequeue();
            element.PutBrick(target, speed);
            CurrentCount++;

            _saveData.name.Add(element.transform.parent.name);
            string json = JsonUtility.ToJson(_saveData);
            PlayerPrefs.SetString("house", json);

            if (queue.Count <= 0)
            {
                _stageCells[CurrentStage].Remove(materials);
            }

            if (_stageCells[CurrentStage].Count <= 0)
            {
                CurrentStage++;
                CurrentCount = 0;
            }
        }
    }

    public int GetMaterialCount(Materials material)
    {
        return _stageCount[CurrentStage][material];
    }

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
