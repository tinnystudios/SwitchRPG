using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponSelection : MonoBehaviour
{
    public Image m_Image;
    public Image m_BG;
    public CanvasGroup m_CanvasGroup;
    public Weapon m_Weapon;

    public void OnSelected()
    {
        m_CanvasGroup.alpha = 1.0F;
    }

    public void OnDeselected()
    {
        m_CanvasGroup.alpha = 0.5F;
    }
}
