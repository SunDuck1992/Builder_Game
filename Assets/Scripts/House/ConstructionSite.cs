using System.Collections;
using System;
using Unity.VisualScripting;
using UnityEngine;

public class ConstructionSite : MonoBehaviour
{
    [SerializeField] private House _house;
    [SerializeField] private float _delay;
    [SerializeField] private float _speed;

    private Coroutine _coroutine;

    public event Action<int, int> OnBuild;

    private void Start()
    {
        OnBuild?.Invoke(_house.CurrentCount, _house.MaxCount);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Player>(out Player player))
        {
            _coroutine = StartCoroutine(BuildHouse(player.Inventory));
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<Player>(out Player player))
        {
            if (_coroutine != null)
            {
                StopCoroutine(_coroutine);
            }
        }
    }

    private IEnumerator BuildHouse(Inventory inventory)
    {
        var material = inventory.Material;

        while (inventory.CurrentCount > 0 & _house.IsCanBuild & _house.CheckMaterial(material))
        {
            _house.BuildElement(inventory.GetItems(), _speed, material);
            OnBuild?.Invoke(_house.CurrentCount, _house.MaxCount);

            yield return new WaitForSeconds(_delay);
        }
    }
}
