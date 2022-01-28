using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    [SerializeField] private Text _hpText;
    [SerializeField] private Text _ammoText;
    [SerializeField] private Button _restartButton;
    [SerializeField] private Button _exitButton;
    [SerializeField] private Text _debugButtonText;
    [SerializeField] private RectTransform _topPanel;
    public float TopBorderY { get => _topPanel.position.y;  }
    public void SetHP(int hp)
    {
        _hpText.text = $"HP - {hp}";
    }
    public void SetAmmo(int ammo)
    {
        _ammoText.text = $"Ammo - {ammo}";
    }
    public void ShowRestart()
    {
        _restartButton.gameObject.SetActive(true);
    }
    public void HideRestart()
    {
        _restartButton.gameObject.SetActive(false);
    }
    public void Exit()
    {
        Application.Quit();
    }
    public void DebugViewSetActive(bool isActive)
    {
        var OnOff = isActive ? "On" : "Off";
        _debugButtonText.text = $"Debug view {OnOff}";
    }
}
