using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CalmApe : GAction
{
    public override bool PrePerform()
    {
        return true;
    }

    public override bool PostPerform()
    {
        GameObject.Find("Gorilla").GetComponent<Gorilla>().enabled = false;
        GameObject.Find("Gorilla").GetComponent<Gorilla>().ResetHunger();
        GameObject.Find("Gorilla").GetComponent<Gorilla>().ResetPlan();
        GameObject.Find("Gorilla").GetComponent<NavMeshAgent>().enabled = false;
        return true;
    }
}
