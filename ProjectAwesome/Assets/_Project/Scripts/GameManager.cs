using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static Action<PlayerController> OnNewPlayer;
    public static Action<PlayerStatus> OnTakeDamage;
    public static Action<EnemyStatus> OnEnemyTakeDamage;
}
