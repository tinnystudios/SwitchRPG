using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chamilion : MonoBehaviour
{
    public LayerMask hitMask;

    public float damageRange = 15;
    public float m_FollowRange = 9;
    public float m_AttackRange = 5;
    private bool isAttacking;

    public float m_MoveSpeed = 5;
    public float m_AttackDuration = 3.0F;
    public float jumpHeight = 10;

    public float coolDownMin = 1.5F;
    public float coolDownMax = 4.0F;

    private bool canAttack = true;

    public AnimationCurve m_YCurve;
    public AnimationCurve m_XZCurve;

    public Rigidbody m_Rigidbody;
    public Transform damageSphere;

    public Collider m_Collider;
    private void Awake()
    {
        SetDamageSphere(3);
    }

    public void SetDamageSphere(float scale)
    {
        damageSphere.transform.localScale = new Vector3(scale, 0.1F, scale);
    }

    private void Update()
    {
        var player = FindObjectOfType<PlayerController>();

        var dist = Vector3.Distance(transform.position, player.transform.position);

        if (dist <= m_AttackRange)
        {
            if (canAttack)
                StartCoroutine(AttackPlayer(player.transform));
        }
        else if (dist <= m_FollowRange && canAttack)
        {
            var dir = player.transform.position - transform.position;
            dir.Normalize();
            transform.position += Time.deltaTime * m_MoveSpeed * dir;

            var lookAtPosition = player.transform.position;
            lookAtPosition.y = transform.position.y;
            transform.LookAt(lookAtPosition);
        }

    }

    //Move to player

    IEnumerator AttackPlayer(Transform player)
    {
        canAttack = false;
        m_Rigidbody.useGravity = false;

        var dir = player.position - transform.position;
        dir.Normalize();

        var position = player.transform.position - (transform.forward * 2);
        position.y += jumpHeight;
        yield return MoveToPosition(position, m_AttackDuration);

        m_Rigidbody.useGravity = true;

        yield return StartCoroutine(DoAttack());

        yield return new WaitForSeconds(Random.Range(coolDownMin, coolDownMax));

        canAttack = true;
    }

    IEnumerator MoveToPosition(Vector3 newPosition, float length)
    {
        var a = transform.position;
        var b = new Vector3(newPosition.x, 0, newPosition.z);
        b.y = transform.position.y;
        transform.LookAt(b);

        for (float i = 0; i < 1.0F; i += Time.deltaTime / length)
        {
            var y = Mathf.Lerp(a.y, newPosition.y, m_YCurve.Evaluate(i));
            var output = new Vector3(newPosition.x, y, newPosition.z);
            transform.position = Vector3.Lerp(a, output, m_XZCurve.Evaluate(i));
            yield return null;
        }

        CheckAttack();
    }

    private void OnDrawGizmos()
    {
        var player = FindObjectOfType<PlayerController>();
        Gizmos.DrawRay(transform.position, transform.forward * m_AttackRange);
        Gizmos.color = Color.blue;
        Gizmos.DrawRay(transform.position + Vector3.up * 0.1F, transform.forward * m_FollowRange);
    }


    public void CheckAttack()
    {
        var player = FindObjectOfType<PlayerController>();

        var dist = Vector3.Distance(transform.position, player.transform.position);

        if (dist <= damageRange/2)
        {
            player.TakeDamage();
        }
    }

    IEnumerator DoAttack()
    {
        for (float i = 0; i < 1.0F; i += Time.deltaTime / 0.15F)
        {
            SetDamageSphere(Mathf.Lerp(3, damageRange, i));
            yield return null;
        }

        CheckAttack();
        SetDamageSphere(3);
    }
}

//Need shooting enemies too
//Need bat