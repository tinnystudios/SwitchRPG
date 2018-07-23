using System;
using System.Collections;
using UnityEngine;

public class MeleeAction : AIAction
{
    public GameObject m_AttackObject;

    public override bool CanPerform
    {
        get
        {
            return true; //Requires some kind of cost
        }
    }

    public override void Perform()
    {
        StartCoroutine(AttackAnimation());
        StartCoolDown();
    }

    IEnumerator AttackAnimation()
    {
        m_AttackObject.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.2F);
        m_AttackObject.gameObject.SetActive(false);
    }
}
