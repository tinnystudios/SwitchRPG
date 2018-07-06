using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FoeUI : MonoBehaviour
{
    public GameObject m_Content;

    public Text m_HP;
    public Text m_EnemyName;
    public Image m_HPBar;

    private void Awake()
    {
        GameManager.OnEnemyTakeDamage += OnEnemyTakeDamage;
        m_Content.SetActive(false);
    }

    private void OnEnemyTakeDamage(EnemyStatus obj)
    {
        StopAllCoroutines();
        StartCoroutine(CoolDown());

        m_HP.text = obj.mHP.ToString();
        m_EnemyName.text = obj.mName;
        m_HPBar.fillAmount = obj.PercentageRemaining;
    }

    IEnumerator CoolDown()
    {
        m_Content.SetActive(true);
        yield return new WaitForSeconds(5);
        m_Content.SetActive(false);
    }
}
