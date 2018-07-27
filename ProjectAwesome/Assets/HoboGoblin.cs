using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoboGoblin : MonoBehaviour, ICoolDownable, IStunable {

    public HoboGoblinAnimator mAnimator;
    public float attackRange = 5;
    public float m_FollowRange = 10;


    public float m_MinMoveSpeed = 5;
    public float m_MaxMoveSpeed = 10;
    public AnimationCurve m_MoveCurve; 

    private States mState;
    public States State
    {
        get { return mState; }
    }
    private bool mCoolingDown;

    public void CoolDownMethod()
    {
        StartCoroutine(CoolDown());
    }

    IEnumerator CoolDown()
    {
        mCoolingDown = true;
        yield return new WaitForSeconds(CoolDownTime);
        mCoolingDown = false;
    }

    public float CoolDownTime
    {
        get
        {
            return 5.0F;
        }
    }

    // Update is called once per frame
    void Update ()
    {
        if (State == States.Stunned)
            return;

        if (mCoolingDown)
            return;

        var player = PlayerController.Instance;

        var lookAtPosition = player.transform.position;
        lookAtPosition.y = transform.position.y;
        transform.LookAt(lookAtPosition);

        var dist = Vector3.Distance(transform.position, player.transform.position);

        if (dist <= attackRange)
        {
            mAnimator.Attack();
            mState = States.Attack;
            CoolDownMethod();
        }
        else if (dist <= m_FollowRange)
        {
        
            var dir = player.transform.position - transform.position;
            dir.Normalize();
            
            var t = dist / m_FollowRange;
            var outT = Mathf.Lerp(1, 0, t);

            //if (m_Animator != null)
            //    m_Animator.speed = 1 + (m_MoveCurve.Evaluate(outT) * m_MaxMoveSpeed * 0.25F);

            var outMoveSpeed = Mathf.Lerp(m_MinMoveSpeed, m_MaxMoveSpeed, m_MoveCurve.Evaluate(outT));

            transform.position += Time.deltaTime * outMoveSpeed * dir;

            mState = States.Walk;
        }
	}

    public void DoDamage()
    {

    }

    public void Stun(Character character)
    {
        Debug.Log("Stunned");
        StartCoroutine(DoStun());
    }

    IEnumerator DoStun()
    {
        mState = States.Stunned;

        mAnimator.Stunned();
        yield return new WaitForSeconds(2);
        mAnimator.Unstunned();

        mState = States.Idle;
    }

    public enum States
    {
        Idle,
        Attack,
        Stunned,
        Walk
    }
}
