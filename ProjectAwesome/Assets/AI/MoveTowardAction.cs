using UnityEngine;

public class MoveTowardAction : MovementAction
{
    [Header("Move Toward")]
    public float m_StopRange = 2;

    public override bool IsDone
    {
        get
        {
            return base.IsDone || DistToTarget <= m_StopRange;
        }
    }

    public override void Perform()
    {
        m_MoveComponent.Move(DirToTarget);
    }

}