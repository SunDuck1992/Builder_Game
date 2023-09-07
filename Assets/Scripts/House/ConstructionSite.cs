using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ConstructionSite : MonoBehaviour
{
    [SerializeField] private Inventory _inventory;
    [SerializeField] private Player _player;
    [SerializeField] private House _house;
    [SerializeField] private float _delay;
    [SerializeField] private float _speed;

    private Coroutine _coroutine;

    private int _currentNumber => _inventory.CurrentCount;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Player>())
        {
            _coroutine = StartCoroutine(BuildHouse());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Player>())
        {
            if (_coroutine != null)
            {
                StopCoroutine(_coroutine);
            }
        }
    }

    private IEnumerator BuildHouse()
    {
        while (_currentNumber > 0)
        {
            GameObject currentPlayerBrick = _inventory.GetItems(_currentNumber);

            GameObject currentHouseBrick = _house.GetCell(_house.BuiltBricks);

            if(currentPlayerBrick == null  || currentHouseBrick == null)
            {
                yield break;
            }

            _inventory.RemoveItem(currentPlayerBrick);

            Vector3 startPosition = currentPlayerBrick.transform.position;
            Quaternion startRotation = currentPlayerBrick.transform.rotation;

            Vector3 endPosition = currentHouseBrick.transform.position;
            Quaternion endRotation = currentHouseBrick.transform.rotation;

            Vector3 startScale = currentPlayerBrick.transform.localScale;
            Vector3 endScale = currentHouseBrick.transform.localScale;

            float startTime = Time.time;
            float journeyLength = Vector3.Distance(startPosition, endPosition);

            while (Vector3.Distance(startPosition, endPosition) > 0)
            {
                float distCovered = (Time.time - startTime) * _speed;
                float fracJourney = distCovered / journeyLength;

                currentPlayerBrick.transform.position = Vector3.Lerp(startPosition, endPosition, fracJourney);
                //currentPlayerBrick.transform.position = Vector3.MoveTowards(startPosition, endPosition, _speed * Time.deltaTime);
                currentPlayerBrick.transform.rotation = Quaternion.Lerp(startRotation, endRotation, fracJourney);

                currentPlayerBrick.transform.localScale = Vector3.Lerp(startScale, endScale, fracJourney);

                startPosition = currentPlayerBrick.transform.position;
                startScale = currentPlayerBrick.transform.localScale;
                yield return null;
            }

            _house.ChangeQuantityBricks();

            yield return new WaitForSeconds(_delay);
        }
    }
}
