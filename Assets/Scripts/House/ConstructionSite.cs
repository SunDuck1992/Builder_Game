using System.Collections;
using System;
using Unity.VisualScripting;
using UnityEngine;
using System.Linq;
using UnityEngine.InputSystem.Interactions;

public class ConstructionSite : MonoBehaviour
{
    [SerializeField] private House _house;
    [SerializeField] private float _delay;
    [SerializeField] private float _speed;

    private Coroutine _coroutine;

    public event Action<Materials,int, int> OnBuild;
    public event Action OnComplete;
    public House House => _house;

    private void Start()
    {
   
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

       while (inventory.CurrentCount > 0 & _house.IsCanBuild & _house.StageMaterials.Contains(material))
        {
            _house.BuildElement(inventory.GetItems(), _speed, material);
            var info = _house.GetCountInfo(material);
            OnBuild?.Invoke(material ,info.current, info.max);

            if(_house.StageMaterials.Count <= 0)
            {
                _house.NextStage();
            }

            yield return new WaitForSeconds(_delay);
        }
    }
}
