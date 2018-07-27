using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// All BasicAI can move and perform
/// </summary>
public class BasicAI : MonoBehaviour, IStunable, IPushable {

    private Planner m_Planner;
    private List<AIAction> m_CurrentPlan;

    //Still need to have a roaming/idle state
    public GoalState m_AggressiveState;
    public GoalState m_CalmState;

    private GoalState mGoalState;

    [Header("Components")]
    public Transform m_Target;
    public MoveTowardAction m_MoveTowardAction;
    public BasicSensor m_Sensor;

    private bool mStunned = false;

    private void Awake()
    {
        var actions = GetComponentsInChildren<AIAction>();
        foreach (var action in actions) { action.SetTarget(m_Target); }
        m_Planner = new Planner();
        mGoalState = m_AggressiveState;
        StartCoroutine(ProcessUpdate());
    }

    IEnumerator ProcessUpdate()
    {
        while (true)
        {
            m_Sensor.ProcessSensor();

            if (!m_Sensor.SeenPlayer)
            {
                yield return null;
            }

            var actions = m_Planner.Plan(mGoalState, this);
            m_CurrentPlan = actions;

            foreach (var action in actions)
            {
                while (!action.IsDone)
                {
                    action.Perform();
                    yield return null;
                }

                if (action.IsFault)
                {
                    Debug.Log("An action has failed, let's replan");
                    break;
                }

                //Delay until next action
                yield return new WaitForSeconds(action.PerformCost);
            }

            //If you have already detected player
            if (actions.Count == 0)
            {
                SwitchPlan();
            }

            yield return null;
        }
    }

    public void SwitchPlan()
    {
        if (mGoalState == m_CalmState)
            mGoalState = m_AggressiveState;
        else
            mGoalState = m_CalmState;
    }

    public void Stun(Character character)
    {
        if (CharacterHasEnoughCombo(character))
        {
            StopAllCoroutines();
            StartCoroutine(StunCoolDown());
        }
    }

    IEnumerator StunCoolDown()
    {
        mStunned = true;
        yield return new WaitForSeconds(1);
        mStunned = false;

        StartCoroutine(ProcessUpdate());
    }

    public void Push(Character character)
    {
        if(CharacterHasEnoughCombo(character))
            transform.position += character.transform.forward * 2;
    }

    public bool CharacterHasEnoughCombo(Character character)
    {
        var combo = character.GetComponentInChildren<ICombo>();
        return combo != null && combo.CurrentCombo >= 2;
    }
}

