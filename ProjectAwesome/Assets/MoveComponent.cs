﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveComponent : MonoBehaviour
{
    public float m_MoveSpeed; 

    public void Move(Vector3 dir)
    {
        transform.position += Time.deltaTime * dir * m_MoveSpeed;
    }
}
