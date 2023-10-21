using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    [SerializeField] private Transform _handPosition;
    [SerializeField] private FollowText _text;
    [SerializeField] private FXResourses _fxData;

    private LinkedList<GameObject> _itemsList = new();
    private readonly float _distanceBetween = 0.25f;

    public int CurrentCount => _itemsList.Count;

    public event Action<int, int> OnAdd;

    public Materials Material { get; private set; }

    private void Start()
    {
        UpdateCounter();
    }

    public void AddItem(GameObject item, Materials material)
    {

        if(Material == Materials.None)
        {
            Material = material;
        }

        if (_itemsList.Count < UpgradePlayer.Instance.MaxCount)
        {
            SortList(item);
            _itemsList.AddLast(item);

            var fx = PoolService.Instance.FxPool.Spawn(FXType.Build);
            fx.transform.position = item.transform.position;
            StartCoroutine(FxPlay(fx.GetComponent<ParticleSystem>()));

            UpdateCounter();
        }

        if (Material != material)
            return;
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


        //item.transform.localScale = Vector3.one * 36;
    }

    public Transform GetItems()
    {
        GameObject item = _itemsList.Last.Value;
        _itemsList.RemoveLast();
        item.transform.SetParent(null);

        if(_itemsList.Count <= 0)
        {
            Material = Materials.None;
        }

        UpdateCounter();

        return item.transform;
    }

    public void ThrowOutItem()
    {
        GameObject item = _itemsList.Last.Value;
        _itemsList.RemoveLast();

        PoolService.Instance.GetPool(item).DeSpawn(item);

        Material = Materials.None;

        UpdateCounter();
    }

    private void UpdateCounter()
    {
        OnAdd?.Invoke(_itemsList.Count, UpgradePlayer.Instance.MaxCount);
    }

    private void OnEnable()
    {
        UpgradePlayer.Instance.OnUpgrade += UpdateCounter;
    }

    private void OnDisable()
    {
        UpgradePlayer.Instance.OnUpgrade -= UpdateCounter;
    }

    private IEnumerator FxPlay(ParticleSystem particle)
    {
        while (true)
        {
            yield return null;

            if (!particle.isPlaying)
            {
                PoolService.Instance.FxPool.Despawn(particle);
                break;
            }
        }
    }
}
