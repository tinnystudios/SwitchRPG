using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// All BasicAI can move and perform
/// </summary>
public class BasicAI : MonoBehaviour {

    private Planner m_Planner;
    private List<AIAction> m_CurrentPlan;

    public GoalState m_GoalState;
    public MoveTowardAction m_MoveTowardAction;

    public Transform m_Target;

    private void Awake()
    {
        var actions = GetComponentsInChildren<AIAction>();
        foreach (var action in actions) { action.SetTarget(m_Target); }

        m_Planner = new Planner();
        StartCoroutine(ProcessUpdate());
    }

    IEnumerator ProcessUpdate()
    {
        while (true)
        {
            var actions = m_Planner.Plan(m_GoalState, this);
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

            yield return null;
        }
    }

}

