using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Idle : GAction
{
    public override bool PrePerform()
    {
        return true;
    }

    public override bool PostPerform()
    {
        agent.ResetPath();
        return true;
    }
}
