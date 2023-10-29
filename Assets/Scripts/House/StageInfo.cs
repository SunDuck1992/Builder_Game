using System.Collections.Generic;
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
