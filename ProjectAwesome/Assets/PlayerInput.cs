using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{



}

public static class GameInput
{
    public static Vector2 WeaponAxis
    {
        get
        {
            var x = Input.GetAxis("WeaponX");
            var y = Input.GetAxis("WeaponY");

            var xy = new Vector2(x, y);
            return xy;
        }
    }
}