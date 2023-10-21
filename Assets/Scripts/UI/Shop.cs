using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    public void OnClickShop(bool isClick)
    {
        _animator.SetBool("isClick", isClick);
    }
}
