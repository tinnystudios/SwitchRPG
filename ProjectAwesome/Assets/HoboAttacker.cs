using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoboAttacker : MonoBehaviour
{
    public float damageRange = 5;

    public void DoDamage()
    {
        CheckAttack();
    }

    public void CheckAttack()
    {
        var player = FindObjectOfType<PlayerController>();

        var dist = Vector3.Distance(transform.position, player.transform.position);

        if (dist <= damageRange)
        {
            player.TakeDamage();
        }
    }
}
