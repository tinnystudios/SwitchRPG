using UnityEngine;

public class MoveTowardAction : AIAction
{
    public float m_Range = 10;
    public float m_MoveSpeed = 3;

    public override bool CoolingDown
    {
        get { return false; }
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

            return dist <= m_Range;
        }
    }

    public override void Perform()
    {
        var player = PlayerController.Instance;
        var dir = player.transform.position - transform.position;
        transform.position += dir * m_MoveSpeed * Time.deltaTime;
    }
}