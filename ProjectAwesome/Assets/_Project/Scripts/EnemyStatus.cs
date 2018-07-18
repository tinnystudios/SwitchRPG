using UnityEngine;

public class EnemyStatus : Status
{
    public override void TakeDamage(int amount)
    {
        base.TakeDamage(amount);

        if (GameManager.OnEnemyTakeDamage != null)
        {
            GameManager.OnEnemyTakeDamage.Invoke(this);
        }

        var flicker = GetComponentInChildren<IFlicker>();
        if (flicker != null)
            flicker.FlickerColor(Color.red);
    }
}
