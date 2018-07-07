using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public float duration = 2.0F;
    public Vector3 m_Offset;
    public float pause = 0.5F;

    private void Awake()
    {
        StartCoroutine(Loop());
    }

    IEnumerator Loop()
    {
        while (true)
        {
            yield return Move(m_Offset);
            yield return new WaitForSeconds(pause);
            yield return Move(-m_Offset);
            yield return new WaitForSeconds(pause);
        }
    }

    IEnumerator Move(Vector3 offset)
    {
        var a = transform.localPosition;
        var b = a + offset;

        for (float i = 0; i < 1.0f; i += Time.deltaTime / duration)
        {
            transform.localPosition = Vector3.Lerp(a, b, i);
            yield return null;
        }
    }
}
