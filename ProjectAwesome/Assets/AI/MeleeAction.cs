using System;
using System.Collections;
using UnityEngine;

public class MeleeAction : CoolDownAction
{
    public GameObject m_AttackObject;
    public bool mAttacked = false;

    public override bool IsDone
    {
        get
        {
            return mAttacked;
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
        mAttacked = true;
    }

    public override void Reset()
    {
        mAttacked = false;
        base.Reset();
    }
}
