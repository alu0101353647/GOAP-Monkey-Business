using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakOut : GAction
{
    public override bool PrePerform()
    {
        return true;
    }

    public override bool PostPerform()
    {
        agent.Warp(GameObject.Find("GorillaTPOut").transform.position);
        GWorld.Instance.GetWorld().ModifyState("out", 1);
        GWorld.Instance.GetWorld().RemoveState("in");
        agent.ResetPath();
        return true;
    }
}
