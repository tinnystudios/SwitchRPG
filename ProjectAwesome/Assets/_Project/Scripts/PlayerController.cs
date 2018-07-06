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

        if(delta.sqrMagnitude > 0.2F)
            transform.forward = delta;
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

    public void ProcessInput()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            if (mIAbility != null)
            {
                mIAbility.Do();
            }
        }

        if (Input.GetButtonDown("Jump"))
        {
            mRigidBody.velocity = new Vector3(0, m_JumpForce, 0);
        }

        if (Input.GetButtonDown("Fire2"))
        {
            mRigidBody.velocity = Movement * m_DashForce;
        }

        if (Input.GetButtonDown("Fire3"))
        {
            CancelPassive();
            m_SwitchController.Switch();
        }

    }

    public void TakeDamage()
    {
        m_PlayerStatus.TakeDamage(1);

        if (GameManager.OnTakeDamage != null)
            GameManager.OnTakeDamage.Invoke(m_PlayerStatus);
    }
}
