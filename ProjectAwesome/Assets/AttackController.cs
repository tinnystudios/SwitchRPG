using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackController : MonoBehaviour, IAbility
{
    public Animator mAnimator;

    public void Do()
    {
        mAnimator.SetTrigger("Attack");
    }

}

public interface IAbility
{
    void Do();
}