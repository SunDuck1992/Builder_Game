using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstructionSite : MonoBehaviour
{
    [SerializeField] private Inventory _inventory;
    [SerializeField] private Player _player;
    [SerializeField] private House _house;
    [SerializeField] private float _delay;

    private Coroutine _coroutine;
    private int _bricksCollected = 0;

    private int _currentNumber => _inventory.CurrentCount;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Player>())
        {
            _coroutine = StartCoroutine(PutDownBrick());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<Player>())
        {
            StopCoroutine(_coroutine);
        }
    }

    private IEnumerator PutDownBrick()
    {
        while(_currentNumber > 0)
        {
            yield return new WaitForSeconds(_delay);

            GameObject deleteBrick = _inventory.GetItems(_currentNumber - 1);
            _inventory.RemoveItem(deleteBrick);
            Destroy(deleteBrick);

            _bricksCollected++;

            Building();
        }
    }

    private void Building()
    {
        if(_bricksCollected <= _house.FullCountBricks)
        {
            _house.ActivateCell(_bricksCollected);
        }
    }
}
