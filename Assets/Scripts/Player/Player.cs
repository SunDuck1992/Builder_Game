using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Inventory))]
[RequireComponent(typeof(PlayerMovement))]

public class Player : MonoBehaviour
{
    [SerializeField] private int _money;

    private Inventory _inventory;

    public Inventory Inventory => _inventory;

    private void Start()
    {
        _inventory = GetComponent<Inventory>();
        UpgradePlayer.Instance.ChangeMoney(_money);
    }



}
