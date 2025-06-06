using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gorilla : GAgent
{
    [Tooltip("How hungry the gorilla gotta be to break out")]
    public int hungerThreshold = 2;
    [Tooltip("In how many seconds the hunger will tick up")]
    public float hungerTickSecs = 5;

    private int hungerTicks = 0;
    private void Awake()
    {
        goals.Add(new SubGoal("satisfied", 0, true), 1);
        GWorld.Instance.GetWorld().ModifyState("in", 1);
        Invoke("IncreaseHunger", hungerTickSecs);
    }

    public void ResetHunger()
    {
        beliefs.RemoveState("hunger");
        goals.Remove(new SubGoal("satisfied", 0, true));
        hungerTicks = 0;
        Invoke("IncreaseHunger", hungerTickSecs);
    }

    void IncreaseHunger()
    {
        if (!goals.ContainsKey(new SubGoal("satisfied", 0, true)))
        {
            goals.Add(new SubGoal("satisfied", 0, true), 1);
        }
        hungerTicks++;
        if (hungerTicks >= hungerThreshold)
        {
            beliefs.ModifyState("hunger", 1);
        } else
        {
            Invoke("IncreaseHunger", hungerTickSecs);
        }
    }
}
