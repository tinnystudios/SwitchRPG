using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    public Rigidbody m_RigidBody;
    public LayerMask hitMask;

    private float mSize = 0.5F;
    private float mRange = 5;
    private Vector3 mFiredPosition;

    public void Fire(Vector3 dir, float force, float size, float range)
    {
        m_RigidBody.velocity = dir * force;
        StartCoroutine(DestroyOverTime(5));
        transform.localScale = Vector3.one * size;
        mSize = size;
        mRange = range;
        mFiredPosition = transform.position;
    }

    void Update()
    {
        if (Vector3.Distance(transform.position, mFiredPosition) >= mRange)
        {
            Destroy(gameObject);
        }

        RaycastHit hit;

        if (Physics.SphereCast(transform.position, mSize, transform.forward, out hit, mSize, hitMask))
        {
            var activatable = hit.transform.GetComponent<IActivatable>();

            if (activatable != null)
            {
                activatable.Activate();
            }

            var enemy = hit.transform.GetComponent<EnemyStatus>();

            if (enemy != null)
            {
                var damage = 1.0F;
                var weakPoint = hit.collider.GetComponent<IWeakPoint>();

                if (weakPoint != null)
                {
                    damage = damage * weakPoint.Percentage;
                }

                enemy.TakeDamage((int)damage);
                Destroy(gameObject);
            }
        }
    }

    IEnumerator DestroyOverTime(float duration)
    {
        yield return new WaitForSeconds(duration);
        Destroy(gameObject);
    }

}
