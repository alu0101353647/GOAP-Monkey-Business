using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guard : GAgent
{
    private void Awake()
    {
        goals.Add(new SubGoal("gaming", 0, true), 1);
        goals.Add(new SubGoal("enclosedApe", 0, false), 2);
    }
}
