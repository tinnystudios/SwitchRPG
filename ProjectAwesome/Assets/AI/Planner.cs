using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planner
{
    public List<AIAction> Plan(GoalState goal, BasicAI agent)
    {
        var actions = agent.GetComponentsInChildren<AIAction>();

        var result = new List<AIAction>();

        //Add actions that are contributing to the goal
        foreach (var action in actions)
        {
            if (action.m_LookUp.ContainsKey(goal))
            {
                if(!action.CoolingDown && action.CanPerform && action.InRange)
                    result.Add(action);
            }
        }

        //Sort actions by effectiveness
        result.Sort((p1, p2) => p2.m_LookUp[goal].effectiveness.CompareTo(p1.m_LookUp[goal].effectiveness));

        return result;
    }


}


public static class ListUtils
{
    public static int SortByScore(GoalStateEffect p1, GoalStateEffect p2)
    {
        return p1.effectiveness.CompareTo(p2.effectiveness);
    }
}