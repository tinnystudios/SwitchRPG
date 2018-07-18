using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class AttackController : MonoBehaviour, IAbility, ICoolDownable
{
    public Animator mAnimator;
    public float range = 2.0F;
    public LayerMask hitMask;
    public float coolDownAmount = 0.5F;

    public float CoolDownTime
    {
        get
        {
            return coolDownAmount;
        }
    }

    public void Do()
    {
        mAnimator.SetTrigger("Attack");
    }

    public void Check()
    {
        var player = FindObjectOfType<PlayerController>();

        RaycastHit hit;

        var nClosest = TargettingUtils.GetNearestTarget<EnemyStatus>(transform);

        if (nClosest != null)
        {
            var dist = Vector3.Distance(transform.position, nClosest.transform.position);
            if (dist <= range)
            {
                nClosest.transform.position += player.transform.forward * 2;
                nClosest.TakeDamage(1);
            }
        }
        var sword = GetComponentInChildren<Sword>();
        if (sword != null)
        {
            var closestBullets = TargettingUtils.GetNearestTarget<Bullet>(transform, 3);

            foreach (var bullet in closestBullets)
            {
                var dist = Vector3.Distance(transform.position, bullet.transform.position);

                if (dist <= 5)
                {
                    bullet.Fire(player.transform.forward,50, 0.5F, 20);
                }
            }
        }
    }

}

public interface IAbility
{
    void Do();
}

public static class TargettingUtils
{

    public static T GetNearestTarget<T>(Transform rootTransform) where T : MonoBehaviour
    {
        var list = GameObject.FindObjectsOfType<T>().ToList();
        return GetNearestTarget(rootTransform, list);
    }

    public static T GetNearestTarget<T>(Transform rootTransform,List<T> list) where T : MonoBehaviour
    {
        var nClosest = list.OrderBy(t => (t.transform.position - rootTransform.position).sqrMagnitude)
                                   .ToArray().FirstOrDefault();

        return nClosest;
    }

    public static T[] GetNearestTarget<T>(Transform rootTransform, int amount) where T : MonoBehaviour
    {
        var list = GameObject.FindObjectsOfType<T>().ToList();
        return GetNearestTarget(rootTransform, list, amount);
    }

    public static T[] GetNearestTarget<T>(Transform rootTransform, List<T> list, int amount) where T : MonoBehaviour
    {
        var nClosest = list.OrderBy(t => (t.transform.position - rootTransform.position).sqrMagnitude)
                                   .Take(amount)
                                   .ToArray();

        return nClosest;
    }

}