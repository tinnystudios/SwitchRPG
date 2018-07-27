using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimation : MonoBehaviour
{
    public Animator mAnimator;

    void LateUpdate()
    {

    }

    public void Run(float speed)
    {

        mAnimator.SetFloat("Input X", speed);
        mAnimator.SetFloat("Input Z", speed);

        //Get local velocity of charcter
        //float velocityXel = transform.InverseTransformDirection(mRigidBody.velocity).x;
        //float velocityZel = transform.InverseTransformDirection(mRigidBody.velocity).z;

        //Update animator with movement values
        //mAnimator.SetFloat("Input X", velocityXel / speed);
        //mAnimator.SetFloat("Input Z", velocityZel / speed);
    }

    public void Walk()
    {

    }

    //Input.GetButtonDown("Jump")
    public void Jump()
    {
        //animator.SetTrigger("JumpTrigger");
        //animator.SetInteger("Jumping", 1);
    }

    public void ChainAttack()
    {
        Attack(1);
    }

    public void Attack(int i)
    {
        mAnimator.SetInteger("Attack", i);
    }

    public void RangeAttack()
    {

    }

    //Input.GetAxis("TargetBlock")
    public void Block(bool state)
    {
        mAnimator.SetBool("Block", state);
    }

    public void BlockHitReact()
    {
        mAnimator.SetTrigger("BlockHitReactTrigger");
    }

    public void GetHit()
    {
        mAnimator.SetTrigger("LightHitTrigger");
    }

    void Dead()
    {
        mAnimator.applyRootMotion = true;
        mAnimator.SetTrigger("DeathTrigger");
    }

}
