using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GACageApe : GAction
{
    Transform apeParent;
    GameObject ape;
    public override bool PrePerform()
    {
        ape = GameObject.Find("Gorilla");
        apeParent = ape.transform.parent;
        ape.transform.parent = transform;
        ape.GetComponent<NavMeshAgent>().enabled = false;
        return true;
    }

    public override bool PostPerform()
    {
        ape.GetComponent<NavMeshAgent>().enabled = true;
        ape.GetComponent<NavMeshAgent>().Warp(GameObject.Find("GorillaTPIn").transform.position);
        ape.transform.parent = apeParent;
        GWorld.Instance.GetWorld().SetState("out", 0);
        agentBeliefs.beliefs.SetState("calmed", 0);
        agentBeliefs.beliefs.SetState("out", 0);
        //ape.GetComponent<GGorilla>().enabled = true;
        //agent.SetDestination(GameObject.Find("IdlingSpot").transform.position);
        //print("-----------APE CAGED-------------");
        return true;
    }
}
