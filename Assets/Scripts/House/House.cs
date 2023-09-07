using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

public class House : MonoBehaviour
{
    [SerializeField] private List<GameObject> _cells = new List<GameObject>();

    private int _builtBricks = 0;

    public int FullCountBricks => _cells.Count;
    public int BuiltBricks => _builtBricks;

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

    public void ChangeQuantityBricks()
    {
        if (_builtBricks < FullCountBricks)
        {
            _builtBricks++;
        }
    }

    public GameObject GetCell(int number)
    {
        if (_builtBricks < FullCountBricks)
        {
            GameObject brick = _cells[number];
            return brick;
        }
        return null;
    }
}
