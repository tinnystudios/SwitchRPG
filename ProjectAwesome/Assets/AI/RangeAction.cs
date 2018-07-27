﻿using UnityEngine;

public class RangeAction : CoolDownAction
{
    public Bullet m_Bullet;
    public float m_MinRange = 1;
    public float force = 15;
    public float size = 0.5F;
    private bool mAttacked = false;

    public override bool MeetConditions
    {
        get
        {
            var dist = Vector3.Distance(Target.position, transform.position);
            return base.MeetConditions && dist >= m_MinRange;
        }
    }

    public override bool IsDone
    {
        get
        {
            return mAttacked;
        }
    }

    public override void Perform()
    {
        var bullet = Instantiate(m_Bullet,transform.position,transform.rotation);
        var dir = Target.position - transform.position;
        dir.Normalize();
        dir.y = Target.position.y;

        bullet.Fire(dir, force, size, 30.0F);
        StartCoolDown();
        mAttacked = true;
    }

    public override void Reset()
    {
        mAttacked = false;
        base.Reset();
    }
}
