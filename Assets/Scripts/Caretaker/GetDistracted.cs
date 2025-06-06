using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetDistracted : GAction
{
    public override bool PrePerform()
    {
        return true;
    }

    public override bool PostPerform()
    {
        beliefs.RemoveState("notificationReceived");
        return true;
    }
}
