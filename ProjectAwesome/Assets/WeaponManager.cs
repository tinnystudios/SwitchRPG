using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public WeaponInventory m_WeaponInventory;

    public CanvasGroup m_WeaponUI;

    public List<WeaponSelection> m_WeaponSelections = new List<WeaponSelection>();
    private WeaponSelection mSelectedWeapon;

    // Update is called once per frame
    void Update ()
    {
        if (GameInput.WeaponAxis.sqrMagnitude != 0)
        {
            m_WeaponUI.alpha = 1.0F;

            var axis = GameInput.WeaponAxis;

            if (axis.y == 1)
            {
                m_WeaponSelections[0].OnSelected();
                mSelectedWeapon = m_WeaponSelections[0];
            }

            if (axis.x == 1)
            {
                m_WeaponSelections[1].OnSelected();
                mSelectedWeapon = m_WeaponSelections[1];
            }

            if (axis.x == -1)
            {
                m_WeaponSelections[2].OnSelected();
                mSelectedWeapon = m_WeaponSelections[2];
            }

            foreach (var weaponSelection in m_WeaponSelections)
            {
                if (weaponSelection != mSelectedWeapon)
                    weaponSelection.OnDeselected();
            }
        }
        else
        {

            if (mSelectedWeapon != null)
            {
                m_WeaponInventory.SelectWeapon(mSelectedWeapon.m_Weapon);
            }

            mSelectedWeapon = null;
            m_WeaponUI.alpha = 0.0F;
        }
    }
}
