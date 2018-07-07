using UnityEngine;

public class RangeAttack : MonoBehaviour, IRangeAttack, ICoolDownable
{
    public PlayerController m_PlayerController;
    public Transform m_ShootingLine;
    public PlayerBullet m_Bullet;
    private bool isActive = false;
    public float shootForce = 50;
    public float coolDownTime = 0.5F;
    public float shootTime = 0.35F;

    private float mStartTime = 0;
    public AnimationCurve mLineCurve;
    public float shootingRange = 20;
    public float minShootingRange = 5;
    private float mProgress;

    public float CoolDownTime
    {
        get
        {
            return coolDownTime;
        }
    }

    public void Show(Vector2 joystickInput)
    {
        if (!isActive)
        {
            mStartTime = Time.time;
        }

        var timeLapsed = Time.time - mStartTime;
        var shootScale = m_ShootingLine.transform.localScale;
        var t = timeLapsed / shootTime;
        mProgress = t;
        //10 meters
        shootScale.z = Mathf.Lerp(minShootingRange, shootingRange, mLineCurve.Evaluate(t));
        m_ShootingLine.transform.localScale = shootScale;

        isActive = true;
        m_ShootingLine.gameObject.SetActive(true);

        var lookDirection = -transform.forward;

        lookDirection.x = joystickInput.x;
        lookDirection.z = -joystickInput.y;

        transform.forward = new Vector3(-lookDirection.x, transform.forward.y, -lookDirection.z);
        m_ShootingLine.forward = transform.forward;


    }

    //joystick input = dir
    //time = power

    public void Hide()
    {
        if (isActive)
        {
            var bullet = Instantiate(m_Bullet, transform.position, transform.rotation);

            var range = Mathf.Lerp(minShootingRange,shootingRange,mProgress);

            bullet.Fire(transform.forward, shootForce, 1.0F, range);
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