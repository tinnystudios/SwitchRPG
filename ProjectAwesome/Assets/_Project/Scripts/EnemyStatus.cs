﻿using System;
using UnityEngine;

public class EnemyStatus : Status
{
    public override void TakeDamage(int amount, int currentCombo = 0)
    {
        base.TakeDamage(amount);

        if (GameManager.OnEnemyTakeDamage != null)
        {
            GameManager.OnEnemyTakeDamage.Invoke(this);
        }

        var flicker = GetComponentInChildren<IFlicker>();
        if (flicker != null)
            flicker.FlickerColor(Color.red);

        if (currentCombo >= 1)
        {
            
            var stunable = GetComponentInChildren<IStunable>();

            if (stunable != null)
            {
                stunable.Stun();
            }

        }
    }
}

public interface IStunable
{
    void Stun();
}
