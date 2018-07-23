using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideAction : AIAction
{
    public float m_MoveSpeed;
    private float startTime = 0;
    public override bool CanPerform
    {
        get
        {
            return true;
        }
    }

    public override void Perform()
    {
        if(startTime == 0)
            startTime = Time.time;

        var timeLapsed = Time.time - startTime;

        if (timeLapsed >= 1.5F)
        {
            startTime = 0;
            StartCoolDown();
        }

        var dir = m_Target.position - transform.position;
        dir.Normalize();
        var dist = Vector3.Distance(m_Target.position, transform.position);

        if(dist < m_Range)
            transform.position -= Time.deltaTime * dir * m_MoveSpeed;
    }
}
