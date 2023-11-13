using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkins : MonoBehaviour
{
    [SerializeField] private List<GameObject> _skins;
    [SerializeField] private Shop _shop;
    [SerializeField] private PlayerMovement _movement;

    private void ChangeSkin(GameObject skin)
    {
        _skins.ForEach(x => x.SetActive(false));
        var activeSkin = _skins.Find(x => x.name.Equals(skin.name));
        activeSkin.SetActive(true);
        _movement.Animator = activeSkin.GetComponent<Animator>();
        PlayerPrefs.SetInt("skin", _skins.IndexOf(activeSkin));
    }

    private void Start()
    {
        _shop = FindObjectOfType<Shop>();
        _shop.OnChangeSkin += ChangeSkin;
        _skins[PlayerPrefs.GetInt("skin", 0)].SetActive(true);
        _movement.Animator = _skins[PlayerPrefs.GetInt("skin", 0)].GetComponent<Animator>();
    }

    private void OnDestroy()
    {
        _shop.OnChangeSkin -= ChangeSkin;
    }
}
