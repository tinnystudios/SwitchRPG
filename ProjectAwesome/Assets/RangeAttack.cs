using UnityEngine;

public class RangeAttack : MonoBehaviour, IRangeAttack
{
    public PlayerController m_PlayerController;
    public Transform m_ShootingLine;
    public PlayerBullet m_Bullet;
    private bool isActive = false;
    public float shootForce = 50;
    public void Show(Vector2 joystickInput)
    {
        isActive = true;
        m_ShootingLine.gameObject.SetActive(true);

        var lookDirection = -transform.forward;

        lookDirection.x = joystickInput.x;
        lookDirection.z = -joystickInput.y;

        transform.forward = new Vector3(-lookDirection.x, transform.forward.y, -lookDirection.z);
        m_ShootingLine.forward = new Vector3(lookDirection.x, transform.forward.y, lookDirection.z);
    }

    //joystick input = dir
    //time = power

    public void Hide()
    {
        if (isActive)
        {
            var bullet = Instantiate(m_Bullet, transform.position, transform.rotation);
            bullet.Fire(transform.forward, shootForce, 1.0F);
        }

        m_ShootingLine.gameObject.SetActive(false);
        isActive = false;

    }

}

public interface IRangeAttack
{
    void Show(Vector2 joystickInput);
    void Hide();
}