using Newtonsoft.Json.Serialization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warehouse : MonoBehaviour
{
    [SerializeField] private Inventory _inventory;
    [SerializeField] private GameObject _brickPrefab;
    [SerializeField] private Transform _handPosition;
    [SerializeField] private float _delay;

    private Coroutine _coroutine;
    private int NumberBrick => _inventory.CurrentCount;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Player>())
        {
            _coroutine = StartCoroutine(PickUpBrick());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.GetComponent<Player>())
        {
            StopCoroutine(_coroutine);
        }
    }

    private IEnumerator PickUpBrick()
    {
        while (NumberBrick < _inventory.MaxCount)
        {
            yield return new WaitForSeconds(_delay);

            GameObject newBrick = Instantiate(_brickPrefab, _handPosition.position, _handPosition.rotation, transform);
            _inventory.AddItem(newBrick);
        }
    }
}
