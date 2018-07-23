using UnityEngine;

public class MoveTowardAction : AIAction
{
    public float m_MoveSpeed = 3;
    public float m_StopRange = 2;
    private float startTime = 0;

    public override bool CanPerform
    {
        get
        {
            return true; //If already in attack ranges
        }
    }

    public override void Perform()
    {
        if (m_Target == null)
            return;


        if (startTime == 0)
            startTime = Time.time;

        var timeLapsed = Time.time - startTime;

        if (timeLapsed >= 1.5F)
        {
            startTime = 0;
            StartCoolDown();
            //In fact it's more like, let's check if there are other actions available so this should be reset as soon as it's the only one
        }

        var dir = m_Target.transform.position - transform.position;
        dir.Normalize();

        var dist = Vector3.Distance(transform.position, m_Target.position);

        if(dist >= m_StopRange)
            transform.position += dir * m_MoveSpeed * Time.deltaTime;
    }
}