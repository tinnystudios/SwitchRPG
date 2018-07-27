using System;
using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Rigidbody m_RigidBody;
    public Action<PlayerController> OnHit;
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
            var status = FindObjectOfType<Character>();
            status.TakeDamage(1);
            Destroy(gameObject);
        }

    }

    IEnumerator DestroyOverTime(float duration)
    {
        yield return new WaitForSeconds(duration);
        Destroy(gameObject);
    }

}
