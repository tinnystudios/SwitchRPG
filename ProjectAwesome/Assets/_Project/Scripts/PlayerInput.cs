using UnityEngine;

public static class PlayerInput
{
    public static Vector3 JoystickLeftInput
    {
        get
        {
            var x = Input.GetAxis("Horizontal");
            var y = Input.GetAxis("Vertical");
            var delta = new Vector3(x, 0, y);

            return delta;
        }
    }

    public static Vector2 JoystickRightInput
    {
        get
        {
            return new Vector2(Input.GetAxis("DashHorizontal"), Input.GetAxis("DashVertical"));
        }
    }

    public static Vector2 DPadInput
    {
        get
        {
            return new Vector2(Input.GetAxis("DpadX"), Input.GetAxis("DpadY"));
        }
    }

    public static bool ChainAttack { get { return Input.GetButtonUp("Fire1"); } }

}