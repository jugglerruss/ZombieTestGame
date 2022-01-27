using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    [SerializeField] private Text _hpText;
    [SerializeField] private Text _ammoText;

    public void SetHP(int hp)
    {
        _hpText.text = $"HP - {hp}";
    }
    public void SetAmmo(int ammo)
    {
        _ammoText.text = $"Ammo - {ammo}";
    }
}
