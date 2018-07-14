using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponInventory : Inventory<Weapon>
{
    public Weapon mActiveWeapon;
    public GameObject mActiveWeaponInstance;

    //just remember last weapon lol...
    private void Start()
    {
        GameManager.OnNewPlayer += OnNewPlayer;
    }

    private void OnNewPlayer(PlayerController obj)
    {
        Debug.Log("new character");
        StartCoroutine(SetWeaponDelay());
    }

    IEnumerator SetWeaponDelay()
    {
        yield return new WaitForEndOfFrame();
        SelectWeapon(mActiveWeapon, ignoreDuplicates: false);
    }

    [ContextMenu("Set Weapon")]
    public void SelectCurrentWeapon()
    {
        SelectWeapon(mActiveWeapon, ignoreDuplicates: false);
    }

    public void SelectWeapon(Weapon weapon, bool ignoreDuplicates = true)
    {
        if (mActiveWeapon == weapon && ignoreDuplicates)
            return;

        if (mActiveWeaponInstance != null)
        {
            Destroy(mActiveWeaponInstance);
        }

        var player = FindObjectOfType<PlayerController>();
        var weaponInstance = Instantiate(weapon.m_Prefab);

        mActiveWeaponInstance = weaponInstance;

        weaponInstance.transform.SetParent(player.weaponPivot);
        weaponInstance.transform.localPosition = Vector3.zero;
        weaponInstance.transform.localRotation = Quaternion.identity;
        weaponInstance.transform.localScale = Vector3.one;
        mActiveWeapon = weapon;
    }
}

public abstract class Inventory<T> : MonoBehaviour
{
    public List<T> m_Inventory = new List<T>();
    public HashSet<T> m_LookUp = new HashSet<T>();

    private void Awake()
    {
        foreach (var t in m_Inventory)
        {
            m_LookUp.Add(t);
        }
    }
}