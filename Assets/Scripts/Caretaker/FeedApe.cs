using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeedApe : GAction
{
    public override bool PrePerform()
    {
        return true;
    }

    public override bool PostPerform()
    {
        GameObject.Find("Gorilla").GetComponent<Gorilla>().ResetHunger();
        return true;
    }
}
