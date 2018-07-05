using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    [Header("Components")]
    public Rigidbody mRigidBody;
    public SwitchController m_SwitchController;
    private IAbility mIAbility;

    [Header("Settings")]
    public float m_MoveSpeed = 3;
    public float m_JumpForce = 5;
    public float m_DashForce = 10;

    private void Awake()
    {
        mIAbility = GetComponentInChildren<IAbility>(includeInactive:true);
    }

    // Update is called once per frame
    void Update ()
    {
        ProcessMovement();
        ProcessInput();
    }

    public void ProcessMovement()
    {
        var x = Input.GetAxis("Horizontal");
        var y = Input.GetAxis("Vertical");
        var delta = new Vector3(x, 0, y);

        transform.position += delta * Time.deltaTime * m_MoveSpeed;

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
            m_SwitchController.Switch();
        }

    }
}
