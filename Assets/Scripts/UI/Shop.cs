using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private List<SkinSettings> _skinSettings;
    [SerializeField] private List<Image> _buttons;
    [SerializeField] private Sprite _sprite;

    public event Action<GameObject> OnChangeSkin;

    private void Start()
    {
        Load();
    }

    public void OnClickShop(bool isClick)
    {
        _animator.SetBool("isClick", isClick);
    }

    public void ChangeSkinButtonClick(int index)
    {

        var skinSettings = _skinSettings[index];

        if (skinSettings.isAds)
        {
            if (PlayerPrefs.HasKey($"skin_{index}"))
            {
                // Если ключ есть, то рекламу не показыввем, просто переключаем скин
            }
            else
            {
                // Если ключа нет, то показываем рекламу и разблокируем скин
            }

            ChangeSkin(skinSettings.gameObjec);
            ChangeButtonSprite(index);
        }
        else
        {
            if (PlayerPrefs.HasKey($"skin_{index}"))
            {
                ChangeSkin(skinSettings.gameObjec);
                ChangeButtonSprite(index);
            }
            else
            {
                if (UpgradePlayer.Instance.CheckMoney(skinSettings.cost))
                {
                    UpgradePlayer.Instance.ChangeMoney(-skinSettings.cost);
                    ChangeSkin(skinSettings.gameObjec);
                    ChangeButtonSprite(index);
                }
            }          
        }     
    }

    private void ChangeSkin(GameObject gameObject)
    {
        OnChangeSkin?.Invoke(gameObject);
    }

    private void ChangeButtonSprite(int index)
    {
        _buttons[index].sprite = _sprite;
        var text = _buttons[index].gameObject.GetComponentInChildren<TextMeshProUGUI>();

        if (text != null)
        {
            text.gameObject.SetActive(false);
        }

        PlayerPrefs.SetString($"skin_{index}", "skin");
    }

    private void Load()
    {
        for (int i = 1; i < _skinSettings.Count; i++)
        {
            if (PlayerPrefs.HasKey($"skin_{i}"))
            {
                ChangeButtonSprite(i);
            }
        }
    }

    [Serializable]
    private class SkinSettings
    {
        public GameObject gameObjec;
        public int cost;
        public bool isAds;
    }
}
