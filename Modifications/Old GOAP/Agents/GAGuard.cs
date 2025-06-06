using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GAGuard : GAgent
{
    GameObject ape;
    void Awake()
    {
        base.Start();

        ape = GameObject.Find("Gorilla");

        agentBeliefs.beliefs.SetState("idling", 0);
        agentBeliefs.beliefs.SetState("calmed", 0);

        goals.Add(new SubGoal("out", 0, false), 1);
        goals.Add(new SubGoal("idling", 1, false), 1);

        //GWorld.Instance.GetWorld().SetState("out", 1);
    }
}
