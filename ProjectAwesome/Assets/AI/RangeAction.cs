using UnityEngine;

public class RangeAction : AIAction
{
    public Bullet m_Bullet;
    public float m_MinRange = 1;

    public override bool CanPerform
    {
        get
        {
            var dist = Vector3.Distance(m_Target.position, transform.position);
            return dist >= m_MinRange;
        }
    }

    public override void Perform()
    {
        Debug.Log("Attacked :: Range");
        var bullet = Instantiate(m_Bullet,transform.position,transform.rotation);
        var dir = m_Target.position - transform.position;
        dir.Normalize();
        bullet.Fire(dir, 25, 0.5F, 30.0F);

        StartCoolDown();
    }
}
