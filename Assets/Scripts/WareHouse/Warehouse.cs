using Newtonsoft.Json.Serialization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warehouse : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _triggerZone;
    [SerializeField] private Color _targetColor;
    [SerializeField] private GameObject _prefabMaterial;
    [SerializeField] private float _delay;
    [SerializeField] private Materials _material;

    private Coroutine _coroutine;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<Player>(out Player player))
        {
            _triggerZone.color = _targetColor;
            _coroutine = StartCoroutine(PickUpBrick(player.Inventory));
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.TryGetComponent<Player>(out Player player))
        {
            _triggerZone.color = Color.white;            

            if (_coroutine != null)
            {
                StopCoroutine(_coroutine);
            }
        }
    }

    private IEnumerator PickUpBrick(Inventory inventory)
    {
        ObjectPool pool = PoolService.Instance.GetPool(_prefabMaterial);

        while (inventory.CurrentCount < UpgradePlayer.Instance.MaxCount & (inventory.Material == Materials.None | inventory.Material == _material))
        {
            yield return new WaitForSeconds(_delay);
        
            inventory.AddItem(pool.Spawn(), _material);
        }
    }
}
