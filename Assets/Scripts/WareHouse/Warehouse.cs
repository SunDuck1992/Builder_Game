using Newtonsoft.Json.Serialization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warehouse : MonoBehaviour
{
    [SerializeField] private GameObject _brickPrefab;
    [SerializeField] private float _delay;
    [SerializeField] private Materials _material;

    private Coroutine _coroutine;

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
        ObjectPool pool = PoolService.Instance.GetPool(_brickPrefab);

        while (inventory.CurrentCount < UpgradePlayer.Instance.MaxCount)
        {
            yield return new WaitForSeconds(_delay);
        
            inventory.AddItem(pool.Spawn(), _material);
        }
    }
}
