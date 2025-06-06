using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeedSelf : GAction
{
    public override bool PrePerform()
    {
        return true;
    }

    public override bool PostPerform()
    {
        beliefs.RemoveState("hunger");
        return true;
    }
}
