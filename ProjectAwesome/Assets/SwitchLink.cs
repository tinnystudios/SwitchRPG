using System.Collections;
using UnityEngine;

public class SwitchLink : MonoBehaviour
{
    public Vector3 localPosition = new Vector3(0, 0, 0);

    private Vector3 startPosition;

    private void Awake()
    {
        startPosition = transform.position;
    }

    [ContextMenu("Drop")]
    public void Activate()
    {
        StartCoroutine(Lower());
    }

    IEnumerator Lower()
    {
        var a = transform.localPosition;

        for (float i = 0; i < 1.0f; i += Time.deltaTime / 2.0F)
        {
            transform.localPosition = Vector3.Lerp(a, localPosition, i);
            yield return null;
        }
    }
}
