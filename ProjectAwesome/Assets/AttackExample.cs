using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackExample : MonoBehaviour
{
    public float range = 5;

    public void CheckAttack()
    {
        RaycastHit hit;

        var nClosest = TargettingUtils.GetNearestTarget<EnemyStatus>(transform,3);

        //GetCurrentMove?


        foreach (var enemy in nClosest)
        {
            var dist = Vector3.Distance(transform.position, enemy.transform.position);
            if (dist <= range)
            {
                var pushable = enemy.GetComponent<IPushable>();

                if (pushable != null)
                {
                    pushable.Push(GetComponent<Character>());
                }

                var stunable = enemy.GetComponentInChildren<IStunable>();

                if (stunable != null)
                {
                    stunable.Stun(GetComponent<Character>());
                }

                enemy.TakeDamage(1);
            }
        }

    }
}

public interface IPushable
{
    void Push(Character character);
}