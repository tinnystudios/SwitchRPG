using UnityEngine;

[CreateAssetMenu(menuName = "GoalState")]
public class GoalState : ScriptableObject
{
    public Goals m_Goal;
    public bool m_State;
}

public enum Goals
{
    Aggressive,
    OutFighter,
    Run,
    Roam
}
