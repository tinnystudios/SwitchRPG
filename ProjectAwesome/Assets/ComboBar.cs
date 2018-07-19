using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComboBar : SingletonMonoBehaviour<ComboBar> {

    public GameObject comboUI;
    public Text m_ComboText;
    public static Action<int> OnCombo;

    public Vector3 min;
    public Vector3 max;

    public RectTransform m_Handle;
    public RectTransform m_End;

    private float value = 0;
    private bool mIsRunning;
    private bool mAttacked;

    private int comboCount = 0;

    public AnimationCurve m_Curve;


    public float center = 0.5F;
    public float deadZone = 0.2F;

    private void Awake()
    {
        max = m_End.anchoredPosition;
        min = m_Handle.anchoredPosition;
        PlayerController.OnPlayerAttack += OnPlayerAttack;
        comboUI.SetActive(false);
    }

    public int ComboCount
    {
        get { return comboCount; }
    }

    public bool IsPlayerAttack
    {
        get
        {
            return mAttacked;
        }
    }

    private void OnPlayerAttack()
    {
        if (!mIsRunning)
            BeginCombo();
        else
        {

            //Reward combo
            if (ComboZone)
            {
                //Continue combo
                comboCount++;
                UpdateComboText();

                if (OnCombo != null)
                    OnCombo.Invoke(comboCount);

            }

            mAttacked = true;
        }
    }

    public bool ComboZone { get { return value > center - deadZone && value < center + deadZone;  }  }

    public void BeginCombo()
    {
        comboCount = 0;
        UpdateComboText();

        StartCoroutine(Run(1.0F));
    }

    IEnumerator Run(float duration)
    {
        comboUI.SetActive(true);
        mIsRunning = true;
        for (float i = 0; i < 1.0F; i += Time.deltaTime / duration)
        {
            m_Handle.anchoredPosition = Vector3.Lerp(min, max, m_Curve.Evaluate(i));

            value = m_Curve.Evaluate(i);

            if (mAttacked)
            {
                mAttacked = false;

                //Reward combo
                if (ComboZone)
                {
                    i = 0;
                }
                else
                {
                    comboCount = 0;
                    break;
                }
            }

            yield return null;
        }

        comboCount = 0;
        mIsRunning = false;
        comboUI.SetActive(false);
    }

    public void UpdateComboText()
    {
        m_ComboText.text = string.Format("COMBO {0} ", comboCount);
    }
}
