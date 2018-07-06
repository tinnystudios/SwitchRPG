using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Turnet : MonoBehaviour
{
    public Transform cannon;

    public bool canRotate = true;

    public Bullet m_BulletPrefab;
    public Transform m_Pivot;
    public float min = 0.5F;
    public float max = 1.5F;
    public float force = 5;
    public float rotationSpeed = 10;

    public Material m_EnergyMaterial;
    public ParticleSystem m_EnergyPS;
    public Color m_ReadyColor;
    public Color m_NormalColor;

    private void Awake()
    {
        StartCoroutine(Shoot());
        ParticleSystemRenderer pr = m_EnergyPS.GetComponent<ParticleSystemRenderer>();
        m_EnergyMaterial = pr.material;
        m_EnergyMaterial.SetColor("_TintColor", m_NormalColor);
    }

    private void Update()
    {
        if (!canRotate)
            return;

        var player = FindObjectOfType<PlayerController>();
        var positionToLookAt = player.transform.position - cannon.position;

        cannon.forward = Vector3.Lerp(cannon.forward,positionToLookAt, rotationSpeed * Time.deltaTime);
    }

    IEnumerator Shoot()
    {
        yield return new WaitForSeconds(1);

        while (true)
        {
            yield return new WaitForSeconds(Random.Range(0.2F,1.0F));

            //canRotate = true;

            float waitDuration = Random.Range(min, max);

            float startTime = Time.time;

            var a = Vector3.one;
            var b = Vector3.one * 3.0F;
            var main = m_EnergyPS.main;

            m_EnergyMaterial.SetColor("_TintColor",m_NormalColor);

            while (Time.time - startTime < waitDuration)
            {
                var timeLapsed = Time.time - startTime;
                var progress = timeLapsed / waitDuration;
                m_EnergyPS.transform.localScale = Vector3.Lerp(a, b, timeLapsed / waitDuration);
                yield return null;
            }

            m_EnergyMaterial.SetColor("_TintColor", m_ReadyColor);

            yield return new WaitForSeconds(0.3F);
            m_EnergyPS.transform.localScale = a;

            var shootAmount = Random.Range(1, 3);

            for (int i = 0; i < shootAmount; i++)
            {
                var bullet = Instantiate(m_BulletPrefab, m_Pivot.position, m_Pivot.rotation, null);
                var dir = cannon.forward;
                dir += new Vector3(Random.Range(-0.2F, 0.2F), Random.Range(-0.05F, 0.05F), Random.Range(-0.2F, 0.2F));
                bullet.Fire(dir, force * Random.Range(0.8F, 1.5F));
                bullet.OnHit += OnHitPlayer;

                yield return new WaitForSeconds(0.1F);
            }


            //canRotate = false;
        }
    }

    IEnumerator Special()
    {
        yield break;
    }

    private void OnHitPlayer(PlayerController player)
    {
        player.TakeDamage();
    }

}
