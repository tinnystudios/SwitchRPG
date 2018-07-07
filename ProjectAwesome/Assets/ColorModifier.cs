using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class ColorModifier : MonoBehaviour
{
    public MeshRenderer m_Renderer;
    public Material m_Ground;
    public Color attackColor = Color.green;

    private void Awake()
    {
        SetCopyColor();
    }


    public void SetAttackColor()
    {
        m_Renderer.sharedMaterial.color = attackColor;
    }

    public void SetCopyColor()
    {
        if (m_Ground != null)
        {
            m_Renderer.sharedMaterial.color = m_Ground.color;
        }
    }
}
