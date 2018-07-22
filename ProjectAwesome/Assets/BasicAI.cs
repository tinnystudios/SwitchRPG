using UnityEngine;

/// <summary>
/// All BasicAI can move and perform
/// </summary>
public class BasicAI : MonoBehaviour {

    public AIAction m_MoveAction;
    public AIAction m_AttackAction;

    private void Update()
    {
        if (m_AttackAction.InRange)
        {
            m_AttackAction.Perform();
        }
        else
        {
            if (m_MoveAction.InRange)
            {
                m_MoveAction.Perform();
            }
        }
    }

}

public class RangeAction : AIAction
{
    public float m_EnterRange, m_PerformRange, m_ExitRange;

    public override bool CoolingDown { get { return false; } }

    public override bool InRange
    {
        get
        {
            return true;
        }
    }

    public override void Perform()
    {
        
    }
}

public abstract class AIAction : MonoBehaviour
{
    public abstract void Perform();
    public abstract bool CoolingDown { get; }
    public abstract bool InRange { get; }
}
