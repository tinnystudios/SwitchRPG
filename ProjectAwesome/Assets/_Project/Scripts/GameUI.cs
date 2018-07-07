using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    public Text m_HP;
    public Image m_HPBar;
    public Image m_PlayerIcon;

    private void Awake()
    {
        GameManager.OnPlayerTakeDamage += OnTakeDamage;
        GameManager.OnNewPlayer += OnNewPlayer;
    }

    private void OnNewPlayer(PlayerController obj)
    {
        UpdateHealthUI(obj.m_PlayerStatus);
        m_PlayerIcon.sprite = obj.m_PlayerUI.m_Image;
    }

    private void OnTakeDamage(PlayerStatus playerStatus)
    {
        UpdateHealthUI(playerStatus);
    }

    public void UpdateHealthUI(PlayerStatus playerStatus)
    {
        m_HP.text = playerStatus.mHP.ToString();
        m_HPBar.fillAmount = playerStatus.PercentageRemaining;
    }
}
