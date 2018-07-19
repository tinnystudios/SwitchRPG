using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoboGoblinAnimator : MonoBehaviour
{
    public Animator m_Animator;

    public void Attack()
    {
        m_Animator.SetTrigger("Attack");
    }

    public void Dead()
    {

    }

    public void Stunned()
    {
        m_Animator.speed = 0;
    }

    public void Unstunned()
    {
        m_Animator.speed = 1;
    }
}
