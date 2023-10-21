using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class HouseProgress 
{
    public List<string> name = new();
    public int currentStage;
    public bool ContainTo(BuildMaterial material)
    {
        return name.Contains(material.transform.parent.name);
    }

    public void Save(BuildMaterial buildMaterial)
    {
        if(buildMaterial !=  null)
        {
            name.Add(buildMaterial.transform.parent.name);
        }
    }
}
