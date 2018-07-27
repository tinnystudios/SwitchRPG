public class PlayerStatus : Status
{
    public override void TakeDamage(int amount, int currentCombo = 0)
    {
        base.TakeDamage(amount, currentCombo);

        if(GameManager.OnPlayerTakeDamage != null)
        GameManager.OnPlayerTakeDamage.Invoke(this);
    }
}
