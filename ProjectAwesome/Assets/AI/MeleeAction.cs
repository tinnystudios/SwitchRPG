using UnityEngine;

public class MeleeAction : AIAction
{
    public float m_AttackRange = 3;

    public override bool CoolingDown
    {
        get
        {
            return false;
        }
    }

    public override bool InRange
    {
        get
        {
            var player = PlayerController.Instance;

            if (player == null)
            {
                return false;
            }

            var dist = Vector3.Distance(transform.position, player.transform.position);

            return dist <= m_AttackRange;
        }
    }

    public override void Perform()
    {
        Debug.Log("Attack");
    }
}
