using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTarget : MonoBehaviour {

    public Transform m_Target;
    public Vector3 m_Offset;
    public float height = 20;
    public float dampSpeed = 5;

	// Update is called once per frame
	void Update ()
    {

        if (m_Target == null || !m_Target.gameObject.activeInHierarchy)
        {
            var player = FindObjectOfType<PlayerController>();
            m_Target = player.transform;
            return;
        }

        var position = m_Target.position;

        position += m_Target.up * height;
        position += m_Offset;

        transform.position = Vector3.Lerp(transform.position,position, dampSpeed * Time.deltaTime); 
	}
}
