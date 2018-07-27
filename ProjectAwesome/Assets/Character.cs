using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public Status m_Status;

    private bool isCoolingDown;

    public void TakeDamage(int amount)
    {
        m_Status.TakeDamage(amount);
        StopActions(0.2F);
    }

    public void StopActions(float coolDownDuration)
    {
        if (!isCoolingDown)
            StartCoroutine(CoolDownProcess(coolDownDuration));
    }

    IEnumerator CoolDownProcess(float coolDOwnDuration)
    {
        isCoolingDown = true;
        yield return new WaitForSeconds(coolDOwnDuration);
        isCoolingDown = false;
    }

}
