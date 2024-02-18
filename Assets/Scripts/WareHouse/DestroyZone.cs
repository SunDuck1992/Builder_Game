using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyZone : MonoBehaviour
{
    [SerializeField] private float _delay;
    [SerializeField] private SpriteRenderer _triggerZone;
    [SerializeField] private Color _targetColor;

    private Coroutine _coroutine;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<Player>(out Player player))
        {
            _triggerZone.color = _targetColor;
            _coroutine = StartCoroutine(DestroyMaterial(player.Inventory));
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

    private IEnumerator DestroyMaterial(Inventory inventory)
    {
        while(inventory.CurrentCount > 0)
        {
            yield return new WaitForSeconds(_delay);

            inventory.ThrowOutItem();
        }
    }
}
