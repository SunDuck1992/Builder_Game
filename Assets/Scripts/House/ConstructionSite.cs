using System.Collections;
using System;
using Unity.VisualScripting;
using UnityEngine;
using System.Linq;
using UnityEngine.InputSystem.Interactions;
using UnityEngine.InputSystem;

public class ConstructionSite : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _triggerZone;
    [SerializeField] private Color _targetColor;
    [SerializeField] private House _house;
    [SerializeField] private float _delay;
    [SerializeField] private float _speed;

    private Coroutine _coroutine;

    public event Action<Materials,int, int> OnBuild;
    public event Action OnCompleteStage;
    public event Action<int, int> OnHouseBuild;
    public event Action OnCompleteBuild;
    public House House => _house;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Player>(out Player player))
        {
            _triggerZone.color = _targetColor;
            _coroutine = StartCoroutine(BuildHouse(player.Inventory));
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<Player>(out Player player))
        {
            _triggerZone.color = Color.white;

            if (_coroutine != null)
            {
                StopCoroutine(_coroutine);
            }
        }
    }

    private IEnumerator BuildHouse(Inventory inventory)
    {
        var material = inventory.Material;
        
        while (inventory.CurrentCount > 0 & _house.IsCanBuild && _house.StageMaterials.Contains(material))
        {
            _house.BuildElement(inventory.GetItems(), _speed, material);
            var info = _house.GetCountInfo(material);
            OnBuild?.Invoke(material ,info.current, info.max);
            OnHouseBuild?.Invoke(_house.CurrnetElementsCount, _house.MaxElementsCount);

            if (_house.CheckIsComplete(() => OnCompleteBuild?.Invoke()))
            {
                var volumeFX = PoolService.Instance.VolumeFXPool.Spawn(VolumeFXType.BuildComplete);
                StartCoroutine(VolumeFxPlay(volumeFX));
            }

            if (_house.StageMaterials.Count <= 0)
            {
                _house.NextStage();
                OnCompleteStage?.Invoke();
            }
            yield return new WaitForSeconds(_delay);
        }
    }

    private IEnumerator VolumeFxPlay(AudioSource audioSource)
    {
        while (true)
        {
            yield return null;

            if (!audioSource.isPlaying)
            {
                PoolService.Instance.VolumeFXPool.Despawn(audioSource);
                break;
            }
        }
    }
}
