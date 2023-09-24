using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

public class House : MonoBehaviour
{
    private Dictionary<Materials, Queue<BuildMaterial>> _cellMaterials = new();
    //private Queue<BuildMaterial> _cellsBricks = new ();

    private HouseProgress _saveData;

    public bool IsCanBuild => _cellMaterials.Count > 0;
    public int MaxCount { get; private set; }
    public int CurrentCount { get; private set; }



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

        var materials = transform.GetComponentsInChildren<BuildMaterial>();

        for (int i = 0; i < materials.Length; i++)
        {
            if (!_saveData.name.Contains(materials[i].name))
            {
                if (!_cellMaterials.TryGetValue(materials[i].Materials, out var queue))
                {
                    queue = new Queue<BuildMaterial>();
                    _cellMaterials.Add(materials[i].Materials, queue);
                }

                queue.Enqueue(materials[i]);
            }
            else
            {
                materials[i].GetComponent<MeshRenderer>().enabled = true;
                CurrentCount++;
            }

        }

        MaxCount = materials.Length;
    }

    public bool CheckMaterial(Materials material)
    {
        return _cellMaterials.ContainsKey(material);
    }

    public void BuildElement(Transform target, float speed, Materials materials)
    {
        if (target == null)
        {
            Debug.Log("return");
            return;
        }

        if (_cellMaterials.TryGetValue(materials, out var queue))
        {
            var material = queue.Dequeue();
            material.PutBrick(target, speed);
            CurrentCount++;

            _saveData.name.Add(material.name);
            string json = JsonUtility.ToJson(_saveData);
            PlayerPrefs.SetString("house", json);

            if (queue.Count <= 0)
            {
                _cellMaterials.Remove(materials);
            }
        }
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
        var bricks = transform.GetComponentsInChildren<BuildMaterial>();

        for (int i = 0; i < bricks.Length; i++)
        {
            bricks[i].GetComponent<MeshRenderer>().enabled = false;
        }
    }

    [ContextMenu("Show")]
    public void ShowBricks()
    {
        var bricks = transform.GetComponentsInChildren<BuildMaterial>();

        for (int i = 0; i < bricks.Length; i++)
        {
            bricks[i].GetComponent<MeshRenderer>().enabled = true;
        }
    }
}
