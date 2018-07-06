using System;
using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Rigidbody m_RigidBody;
    public Action<PlayerController> OnHit;
    public LayerMask hitMask;

    public void Fire(Vector3 dir, float force)
    {
        m_RigidBody.velocity = dir * force;
        StartCoroutine(DestroyOverTime(2));
    }

    void Update()
    {

        RaycastHit hit;

        if (Physics.SphereCast(transform.position, 0.5F, transform.forward, out hit, 0.5F, hitMask))
        {
            var player = FindObjectOfType<PlayerController>();

            if (OnHit != null)
            {
                OnHit.Invoke(player);
            }

            Destroy(gameObject);
        }

    }

    IEnumerator DestroyOverTime(float duration)
    {
        yield return new WaitForSeconds(duration);
        Destroy(gameObject);
    }

}
