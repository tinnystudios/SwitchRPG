using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float m_AttackRange = 5;
    private bool isAttacking;

    public float m_AttackDuration = 3.0F;
    public float jumpHeight = 10;

    public float coolDown = 3;

    private bool canAttack = true;

    public AnimationCurve m_YCurve;
    public AnimationCurve m_XZCurve;

    public Rigidbody m_Rigidbody;

    private void Update()
    {
        var player = FindObjectOfType<PlayerController>();

        var dist = Vector3.Distance(transform.position, player.transform.position);

        if (dist >= m_AttackRange)
        {
            if(canAttack)
                StartCoroutine(AttackPlayer(player.transform));
        }

    }

    IEnumerator AttackPlayer(Transform player)
    {
        canAttack = false;
        m_Rigidbody.useGravity = false;

        var dir = player.position - transform.position;
        dir.Normalize();

        var position = player.transform.position;
        position.y += jumpHeight;
        yield return MoveToPosition(position, m_AttackDuration);

        m_Rigidbody.useGravity = true;

        yield return new WaitForSeconds(coolDown);

        canAttack = true;
    }

    IEnumerator MoveToPosition(Vector3 newPosition, float length)
    {
        var a = transform.position;

        for (float i = 0; i < 1.0F; i += Time.deltaTime/length)
        {
            var y = Mathf.Lerp(a.y, newPosition.y, m_YCurve.Evaluate(i));
            var output = new Vector3(newPosition.x, y, newPosition.z);
            transform.position = Vector3.Lerp(a, output, m_XZCurve.Evaluate(i));
            yield return null;
        }

    }

    private void OnDrawGizmos()
    {
        var player = FindObjectOfType<PlayerController>();
        Gizmos.DrawLine(transform.position, player.transform.position);
    }

}
