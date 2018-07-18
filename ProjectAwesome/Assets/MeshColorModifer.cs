using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshColorModifer : MonoBehaviour, IFlicker
{
    public Renderer m_Renderer;
    private Color cachedColor;
    private Material m_Material;

    private void Awake()
    {
        m_Material = m_Renderer.material;
        cachedColor = m_Material.color;
    }

    public void FlickerColor(Color color)
    {
        StartCoroutine(Flicker(color));
    }

    IEnumerator Flicker(Color color)
    {
        m_Material.color = color;
        yield return new WaitForSeconds(0.2F);
        m_Material.color = cachedColor;
    }
}

public interface IFlicker
{
    void FlickerColor(Color color);
}