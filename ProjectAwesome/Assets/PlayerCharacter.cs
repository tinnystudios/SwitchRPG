using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : Character, ICombo
{
    public TargetSystem m_TargetSystem;
    public float CurrentCombo { get { return 0; } }

    void Update()
    {
        if (PlayerInput.ChainAttack)
        {
            m_TargetSystem.PlayEffect();
        }
    }
}
