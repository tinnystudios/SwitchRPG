using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetSystem : MonoBehaviour
{
    public Transform relativeTarget;
    public Transform target;
    public SpriteRenderer m_TargetSprite;
    public ParticleSystem mParticleSystem;

    private float distance = 2;
    public Vector2 constraint = new Vector2(1, 1);

    public Color m_NormalColor = Color.white;
    public Color m_ActiveColor = Color.white;

    public Vector3 offset;
    public float speed = 8;
    public bool snap = false;

    private void Awake()
    {
        mParticleSystem.gameObject.SetActive(false);
    }

    private void Update()
    {
        //Find distance to nearest object
        RaycastHit hit;
        Ray ray = new Ray(relativeTarget.position, relativeTarget.forward);

        if (Physics.SphereCast(ray, 5, out hit, 1.0F))
        {
            distance = hit.distance - 0.5F;
        }
        else
        {
            distance = 2;
        }

        if (PlayerInput.DPadInput.sqrMagnitude == 0)
        {
            m_TargetSprite.color = m_NormalColor;
            return;
        }

        var computePosition = relativeTarget.position + (relativeTarget.forward * distance);
        var camera = Camera.main;
        target.position = computePosition;
        target.localPosition += offset;

        m_TargetSprite.color = m_ActiveColor;

        var input = PlayerInput.DPadInput;
        var delta = new Vector3(input.x, input.y, 0);

        if (snap)
        {
            offset = delta;
        }

        if (!snap)
        {
            offset += delta * Time.deltaTime * speed;

            var clampOffset = new Vector2(0, 0);
            clampOffset.x = Mathf.Clamp(offset.x, -2, 2);
            clampOffset.y = Mathf.Clamp(offset.y, -2, 2);
            offset = clampOffset;
        }

    }

    public void PlayEffect()
    {
        mParticleSystem.gameObject.SetActive(true);
    }
}
