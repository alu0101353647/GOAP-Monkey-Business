using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CageApe : GAction
{
    Transform apeParent;
    GameObject ape;
    public override bool PrePerform()
    {
        ape = GameObject.Find("Gorilla");
        apeParent = ape.transform.parent;
        ape.transform.parent = transform;
        return true;
    }

    public override bool PostPerform()
    {
        ape.GetComponent<Gorilla>().enabled = true;
        ape.GetComponent<NavMeshAgent>().enabled = true;
        ape.transform.parent = apeParent;
        GWorld.Instance.GetWorld().RemoveState("out");
        GWorld.Instance.GetWorld().ModifyState("in", 1);
        ape.GetComponent<NavMeshAgent>().Warp(GameObject.Find("GorillaDrop").transform.position);

        GetComponent<GAgent>().goals.Add(new SubGoal("gaming", 0, true), 1);
        return true;
    }
}
