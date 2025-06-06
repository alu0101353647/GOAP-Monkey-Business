using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GAFeedSelf : GAction
{
    public override bool PrePerform()
    {
        return true;
    }

    public override bool PostPerform()
    {
        agentBeliefs.beliefs.ToString();
        agentBeliefs.beliefs.SetState("hungry", 0);
        return true;
    }
}
