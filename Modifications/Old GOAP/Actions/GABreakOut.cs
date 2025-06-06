using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GABreakOut : GAction
{
    public override bool PrePerform()
    {
        return true;
    }

    public override bool PostPerform()
    {
        GameObject[] locations = GameObject.FindGameObjectsWithTag("Locations");
        foreach (GameObject loc in locations)
        {
            if (loc.name == "GorillaTPOut")
            {
                agent.ResetPath();
                agent.Warp(loc.transform.position);
                target = loc;
                // I have to set this up here so it doesn't 
                // start teleporting everywhere before it reaches PostPerform
                GWorld.Instance.GetWorld().SetState("out", 1);
                return true;
            }
        }
        Debug.LogError("GorillaTPOut does not exist. Should be tagged with Locations");
        return false;
    }
}
