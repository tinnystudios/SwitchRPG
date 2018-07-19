using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    public PlayerController m_PlayerController;
    public Animator m_Animator;

    private void Awake()
    {
        m_PlayerController.OnStateChanged += OnStateChanged;
    }

    private void OnStateChanged(States obj)
    {
        switch (obj)
        {
            case States.Idle:
                m_Animator.SetTrigger("Idle");
                break;

            case States.Attack:
                //m_Animator.SetTrigger("Idle");
                break;

            case States.Move:
                m_Animator.SetTrigger("Move");
                break;
        }
    }
}
