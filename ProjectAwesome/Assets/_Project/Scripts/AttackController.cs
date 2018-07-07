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
        RaycastHit hit;

        var nClosest = TargettingUtils.GetNearestTarget<EnemyStatus>(transform);

        if (nClosest != null)
        {
            var dist = Vector3.Distance(transform.position, nClosest.transform.position);
            if(dist <= range)
            nClosest.TakeDamage(1);
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
        //get 3 closest characters (to referencePos)
        var nClosest = list.OrderBy(t => (t.transform.position - rootTransform.position).sqrMagnitude)
                                   //.Take(3)   //or use .FirstOrDefault();  if you need just one
                                   .ToArray().FirstOrDefault();

        return nClosest;
    }

}