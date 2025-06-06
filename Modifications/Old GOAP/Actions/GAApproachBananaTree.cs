using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GAApproachBananaTree : GAction
{
    [Tooltip("How close to the spot can the agent be to give it as good")]
    [SerializeField] float looseness = 2f;
    public override bool PrePerform()
    {
        print("Approach banana tree pre perform called");
        agent.SetDestination(target.transform.position);
        return true;
    }

    public override bool PostPerform()
    {
        agentBeliefs.beliefs.SetState("nearBanana", 1);
        print("Just performed post perform in Approach Banana Tree");
        return (Vector3.Distance(transform.position, target.transform.position) <= looseness);
    }
}
