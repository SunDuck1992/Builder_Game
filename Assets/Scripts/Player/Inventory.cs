using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    [SerializeField] private int _maxCount;
    [SerializeField] private Transform _handPosition;
    [SerializeField] private Text _inventoryInfo;

    private List<GameObject> _items = new List<GameObject>();
    private float _distanceBetween = 0.25f;

    public int MaxCount => _maxCount;
    public int CurrentCount => _items.Count;

    private void Update()
    {
        for (int i = 0; i < _items.Count; i++)
        {
            _items[i].transform.position = _handPosition.position + Vector3.up * i * _distanceBetween;
            _items[i].transform.rotation = _handPosition.rotation;
        }

        _inventoryInfo.text = _items.Count.ToString() + " / " + _maxCount.ToString();
    }

    public bool IsFull()
    {
        return _items.Count >= _maxCount;
    }

    public void AddItem(GameObject item)
    {
        if (!IsFull())
        {
            _items.Add(item);
        }
    }

    public void RemoveItem(GameObject item)
    {
        _items.Remove(item);
    }

    public GameObject GetItems(int number)
    {
        return _items[number - 1];
    }
}
