using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static Action<PlayerController> OnNewPlayer;
    public static Action<PlayerStatus> OnPlayerTakeDamage;
    public static Action<EnemyStatus> OnEnemyTakeDamage;

    public static void TakeDamage(Status status)
    {
        if (status is PlayerStatus)
        {
            if (OnPlayerTakeDamage != null)
            {
                OnPlayerTakeDamage.Invoke(status as PlayerStatus);
            }
        }

        if (status is EnemyStatus)
        {
            if (OnEnemyTakeDamage != null)
            {
                OnEnemyTakeDamage.Invoke(status as EnemyStatus);
            }
        }
    } 
}
