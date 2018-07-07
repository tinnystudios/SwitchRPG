using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeController : MonoBehaviour, IPassive
{
    public PlayerController m_PlayerController;
    public float timeModifer = 0.3F;

    void OnDestroy()
    {
        CancelPassiveEffect();
    }

    private void OnDisable()
    {
        CancelPassiveEffect();
    }

    public void CancelPassiveEffect()
    {
        Time.timeScale = 1.0F;
        
    }

    public void ApplyPassiveEffect()
    {
        var dir = m_PlayerController.Movement;
        var force = dir.sqrMagnitude;
        var value = Mathf.Lerp(1, 0.1F, force);

        Time.timeScale = timeModifer;
    }

    public bool CanDo
    {
        get
        {
            return !m_PlayerController.HasAction;
        }
    }
}

public interface IPassive
{
    void ApplyPassiveEffect();
    void CancelPassiveEffect();
    bool CanDo { get; }
}