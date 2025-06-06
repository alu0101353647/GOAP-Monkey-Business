using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GGorilla : GAgent
{
    private bool calledHunger = false;
    [Tooltip("The minimum time that will have to elapse before the hunger meter increases")]
    [SerializeField] private int minCDBetweenHungerTicks;
    [SerializeField] private int maxCDBetweenHungerTicks;
    new void Start()
    {
        base.Start();
        agentBeliefs.beliefs.SetState("hungry", 0);
        GWorld.Instance.GetWorld().SetState("out", 0);

        goals.Add(new SubGoal("hungry", 0, false), 0);
        Invoke("IncreaseHunger", Random.Range(minCDBetweenHungerTicks, maxCDBetweenHungerTicks));
        calledHunger = true;
    }

    void IncreaseHunger()
    {
        int currentHunger = (agentBeliefs.beliefs.GetStateValue("hungry") < 2) ? 1 : 0;
        agentBeliefs.beliefs.ModifyState("hungry", currentHunger);
        if (currentHunger == 1) { Invoke("IncreaseHunger", Random.Range(minCDBetweenHungerTicks, maxCDBetweenHungerTicks)); }
        else calledHunger = false;
    }

    public void ResetHunger()
    {
        agentBeliefs.beliefs.SetState("hungry", 0);
    }

    private void Update()
    {
        if (!calledHunger && agentBeliefs.beliefs.GetStateValue("hungry") < 2)
        {
            Invoke("IncreaseHunger", Random.Range(minCDBetweenHungerTicks, maxCDBetweenHungerTicks));
        }
    }
}
