﻿using System;
using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Rigidbody m_RigidBody;
    public Action<PlayerController> OnHit;
    public LayerMask hitMask;
    private float mSize = 0.5F;

    public void Fire(Vector3 dir, float force, float size)
    {
        m_RigidBody.velocity = dir * force;
        StartCoroutine(DestroyOverTime(5));
        transform.localScale = Vector3.one * size;
        mSize = size;
    }

    void Update()
    {

        RaycastHit hit;

        if (Physics.SphereCast(transform.position, mSize, transform.forward, out hit, mSize, hitMask))
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
