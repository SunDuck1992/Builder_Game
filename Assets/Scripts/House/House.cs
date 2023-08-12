using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

public class House : MonoBehaviour
{
    [SerializeField] private List<GameObject> _cells = new List<GameObject>();

    public int FullCountBricks => _cells.Count;

    private void Start()
    {
        foreach (Transform child in transform)
        {
            if (child.GetComponent<Brick>())
            {
                _cells.Add(child.gameObject);
                child.gameObject.SetActive(false);
            }
        }        
    }

    public void ActivateCell(int number)
    {
        GameObject brick = _cells[number - 1];

        if (brick != null)
        {
            brick.SetActive(true);
        }
    }
}
