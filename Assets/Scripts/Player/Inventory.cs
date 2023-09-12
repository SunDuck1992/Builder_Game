using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    [SerializeField] private int _maxCount;
    [SerializeField] private Transform _handPosition;
    [SerializeField] private FollowText _text;

    private LinkedList<GameObject> _itemsList = new();
    private List<GameObject> _items = new List<GameObject>();
    private float _distanceBetween = 0.25f;

    public int MaxCount => _maxCount;
    public int CurrentCount => _itemsList.Count;

    private void Start()
    {
        _text.InfoText.text = _itemsList.Count.ToString() + " / " + _maxCount.ToString();
    }

    public bool IsFull()
    {
        return _items.Count >= _maxCount;
    }

    public void AddItem(GameObject item)
    {
        if (_itemsList.Count < _maxCount)
        {
            SortList(item);
            _itemsList.AddLast(item);

            _text.InfoText.text = _itemsList.Count.ToString() + " / " + _maxCount.ToString();
        }


    }

    private void SortList(GameObject item)
    {
        if(_itemsList.Count > 0)
        {
            Vector3 offset = _itemsList.Last.Value.transform.position + Vector3.up * _distanceBetween;
            item.transform.position = offset;
        }
        else
        {
            item.transform.position = _handPosition.position;
        }

        item.transform.SetParent(_handPosition);
        item.transform.localRotation = Quaternion.identity;
    }

    //public void RemoveItem(GameObject item)
    //{
    //    _items.Remove(item);
    //}

    public Transform GetItems()
    {
        GameObject item = _itemsList.Last.Value;
        _itemsList.RemoveLast();
        item.transform.SetParent(null);

        _text.InfoText.text = _itemsList.Count.ToString() + " / " + _maxCount.ToString();

        return item.transform;

    }
}
