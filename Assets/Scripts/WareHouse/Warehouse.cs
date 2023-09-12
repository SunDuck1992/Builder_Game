using Newtonsoft.Json.Serialization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warehouse : MonoBehaviour
{
    [SerializeField] private GameObject _brickPrefab;
    [SerializeField] private Transform _handPosition;
    [SerializeField] private float _delay;

    private Coroutine _coroutine;
    private ObjectPool _objectPool;

    private void Start()
    {
        _objectPool = new ObjectPool(_brickPrefab);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<Player>(out Player player))
        {
            _coroutine = StartCoroutine(PickUpBrick(player.Inventory));
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.TryGetComponent<Player>(out Player player))
        {
            if (_coroutine != null)
            {
                StopCoroutine(_coroutine);
            }
        }
    }

    private IEnumerator PickUpBrick(Inventory inventory)
    {
        while (inventory.CurrentCount < inventory.MaxCount)
        {
            yield return new WaitForSeconds(_delay);
        
            inventory.AddItem(_objectPool.Spawn());
        }
    }
}
