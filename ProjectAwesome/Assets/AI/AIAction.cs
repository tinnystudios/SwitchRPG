using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AIAction : MonoBehaviour
{
    public Action m_OnCoolDownFinished;

    public Transform m_Target;
    public float m_Range = 2;

    //The effect is not a scriptable as each AI has different effectiveness for specific moves
    private bool mCoolingDown;
    public float m_CoolDownDuration = 0.5F;

    public float timeCost = 1;
    public List<GoalStateEffect> effects = new List<GoalStateEffect>();
    public Dictionary<GoalState, GoalStateEffect> m_LookUp = new Dictionary<GoalState, GoalStateEffect>();

    public abstract void Perform();
    public abstract bool CanPerform { get; }

    public virtual bool InRange
    {
        get
        {
            if (m_Target == null)
            {
                return false;
            }

            var dist = Vector3.Distance(transform.position, m_Target.position);

            return dist <= m_Range;
        }
    }

    public virtual bool CoolingDown { get { return mCoolingDown; } }

    private void Awake()
    {
        foreach (var effect in effects)
            m_LookUp.Add(effect.m_GoalState, effect);
    }

    private void OnValidate()
    {
        foreach (var effect in effects)
        {
            if(effect.m_GoalState != null)
                effect.name = effect.m_GoalState.name;
        }
    }

    public virtual void StartCoolDown()
    {
        if(!mCoolingDown)
            StartCoroutine(CoolDown(m_CoolDownDuration));
    }

    public virtual IEnumerator CoolDown(float coolDownTime)
    {
        mCoolingDown = true;
        yield return new WaitForSeconds(coolDownTime);
        mCoolingDown = false;

        if (m_OnCoolDownFinished != null)
            m_OnCoolDownFinished.Invoke();
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, m_Range);
    }
}

[System.Serializable]
public class GoalStateEffect
{
    [HideInInspector] public string name = "Effect";
    public GoalState m_GoalState = null;
    public float effectiveness = 1;
}