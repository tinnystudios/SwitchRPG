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

    private void Awake()
    {
        m_Planner = new Planner();
        StartCoroutine(ProcessUpdate());
    }

    IEnumerator ProcessUpdate()
    {
        while (true)
        {
            var actions = m_Planner.Plan(m_GoalState, this);
            m_CurrentPlan = actions;

            if (actions.Count > 0)
            {
                actions[0].Perform();
                yield return new WaitForSeconds(actions[0].timeCost);
            }
            else
            {
                //Roam
            }

            yield return null;
        }
    }

}

