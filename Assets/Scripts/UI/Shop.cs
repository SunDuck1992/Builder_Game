using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    public event Action<GameObject> OnChangeSkin;

    public void OnClickShop(bool isClick)
    {
        _animator.SetBool("isClick", isClick);
    }

    public void ChangeSkinButtonClick(GameObject gameObject)
    {
        OnChangeSkin?.Invoke(gameObject);
    }
}
