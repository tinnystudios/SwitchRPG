using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class AttackController : MonoBehaviour, IAbility
{
    public Animator mAnimator;
    public float range = 2.0F;
    public LayerMask hitMask;

    public void Do()
    {
        mAnimator.SetTrigger("Attack");
        RaycastHit hit;

        var list = FindObjectsOfType<EnemyStatus>();

        //get 3 closest characters (to referencePos)
        var nClosest = list.OrderBy(t => (t.transform.position - transform.position).sqrMagnitude)
                                   //.Take(3)   //or use .FirstOrDefault();  if you need just one
                                   .ToArray().FirstOrDefault();

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