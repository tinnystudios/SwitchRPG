using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    [Header("Components")]
    public Rigidbody mRigidBody;
    public SwitchController m_SwitchController;
    private IAbility mIAbility;

    public PlayerStatus m_PlayerStatus;
    public PlayerUI m_PlayerUI;

    [Header("Settings")]
    public float m_MoveSpeed = 3;
    public float m_JumpForce = 5;
    public float m_DashForce = 10;

    public bool isCoolingDown = false;
    public bool isJoystickRightCoolingDown = false;
    public bool isJoyStickRightActive = false;

    public bool HasAction
    {
        get
        {
            return Movement.sqrMagnitude > 0.0F;
        }
    }

    private void Awake()
    {
        mIAbility = GetComponentInChildren<IAbility>(includeInactive:true);
    }

    private void OnEnable()
    {
        if (GameManager.OnNewPlayer != null)
        {
            GameManager.OnNewPlayer.Invoke(this);
        }
    }


    // Update is called once per frame
    void Update ()
    {
        if (isCoolingDown)
            return;

        ProcessMovement();
        ProcessInput();
        ProcessPassive();
    }

    private void ProcessPassive()
    {
        var passives = GetComponentsInChildren<IPassive>();

        foreach (var passiveEffect in passives)
        {
            if (passiveEffect.CanDo)
                passiveEffect.ApplyPassiveEffect();
            else
                passiveEffect.CancelPassiveEffect();
        }
    }

    private void CancelPassive()
    {
        var passives = GetComponentsInChildren<IPassive>();

        foreach (var passiveEffect in passives)
        {
            passiveEffect.CancelPassiveEffect();
        }
    }

    public void ProcessMovement()
    {
        var x = Input.GetAxis("Horizontal");
        var y = Input.GetAxis("Vertical");
        var delta = new Vector3(x, 0, y);
        var multipler = 1.0F;

        transform.position += delta * m_MoveSpeed * Time.deltaTime;

        if(delta.sqrMagnitude > 0.2F && CanLookAtMoveDirection)
            transform.forward = delta;
    }

    public bool CanLookAtMoveDirection
    {
        get
        {
            if (isJoystickRightCoolingDown || isJoyStickRightActive)
                return false;
            else
                return true;
        }
    }

    public Vector3 Movement
    {
        get {
            var x = Input.GetAxis("Horizontal");
            var y = Input.GetAxis("Vertical");
            var delta = new Vector3(x, 0, y);

            return delta;
        }
    }

    public Vector2 RInput
    {
        get
        {
            return new Vector2(Input.GetAxisRaw("HorizontalR"), Input.GetAxisRaw("VerticalR"));
        }
    }

    public void ProcessInput()
    {

        if (Input.GetButtonDown("Fire1"))
        {
            if (mIAbility != null)
            {
                mIAbility.Do();

                if (mIAbility is ICoolDownable)
                {
                    var coolDownable = (ICoolDownable)mIAbility;
                    CoolDown(coolDownable.CoolDownTime);
                }
            }
        }

        if (Input.GetButtonDown("Jump"))
        {
            mRigidBody.velocity = new Vector3(0, m_JumpForce, 0);
        }

        if (Input.GetButtonDown("Fire2"))
        {
            var force = Movement * m_DashForce;
            force.y = mRigidBody.velocity.y;
            mRigidBody.velocity = force;
        }

        if (Input.GetButtonDown("Fire3"))
        {
            CancelPassive();
            m_SwitchController.Switch();
        }

        //show ray
        var rangeAttack = GetComponent<IRangeAttack>();

        if (rangeAttack != null)
        {

            if (RInput.sqrMagnitude > 0.1F)
            {
                //Joystick down
                rangeAttack.Show(RInput);
                isJoyStickRightActive = true;
            }
            else
            {
                //Joystick down
                rangeAttack.Hide();

                if (isJoyStickRightActive)
                {
                    if (rangeAttack is ICoolDownable)
                    {
                        var coolDownable = (ICoolDownable)rangeAttack;
                        CoolDown(coolDownable.CoolDownTime);
                    }

                    OnJoyStickRightUp();
                }

                isJoyStickRightActive = false;
            }

        }

    }

    public void OnJoyStickRightUp()
    {
        if(!isJoystickRightCoolingDown)
            StartCoroutine(JoystickRightCoolDown(1.0F));
    }

    public void TakeDamage()
    {
        m_PlayerStatus.TakeDamage(1);

        if (GameManager.OnTakeDamage != null)
            GameManager.OnTakeDamage.Invoke(m_PlayerStatus);

        //Stop your movement and cool down
        mRigidBody.velocity = Vector3.zero;

        CoolDown(0.2F);
    }

    public void CoolDown(float coolDownDuration)
    {
        if(!isCoolingDown)
            StartCoroutine(CoolDownProcess(coolDownDuration));
    }

    IEnumerator CoolDownProcess(float coolDOwnDuration)
    {
        isCoolingDown = true;
        yield return new WaitForSeconds(coolDOwnDuration);
        isCoolingDown = false;
    }

    IEnumerator JoystickRightCoolDown(float coolDOwnDuration)
    {
        isJoystickRightCoolingDown = true;
        yield return new WaitForSeconds(coolDOwnDuration);
        isJoystickRightCoolingDown = false;
    }
}


public interface ICoolDownable
{
    float CoolDownTime { get; }
}