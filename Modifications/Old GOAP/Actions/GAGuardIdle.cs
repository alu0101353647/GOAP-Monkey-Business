using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GAGuardIdle : GAction
{
    public override bool PrePerform()
    {
        return true;
    }

    public override bool PostPerform()
    {
        agentBeliefs.beliefs.SetState("idling", 1);
        return true;
    }
}
