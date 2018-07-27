using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameInput
{

    public static Vector2 WeaponAxis
    {
        get
        {
            var x = Input.GetAxis("DpadX");
            var y = Input.GetAxis("DpadY");

            var xy = new Vector2(x, y);
            return xy;
        }
    }
}

/// <summary>
/// Static events for AIs to listen to
/// </summary>
public static class PlayerInputEvents
{
    public static Action OnDash;
    public static Action OnBeginCloseRangeAttack;
    public static Action OnBeginRangeAttack;
}