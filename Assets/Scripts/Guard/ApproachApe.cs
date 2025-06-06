using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApproachApe : GAction
{
    public float distanceToInteract = 2f;
    private float previousDistanceToInteract = 1f;
    private bool getRunning = false;
    public override bool PrePerform()
    {
        getRunning = true;
        beliefs.ModifyState("gaming", -1);
        previousDistanceToInteract = GetComponent<Guard>().distanceToInteract;
        GetComponent<Guard>().distanceToInteract = distanceToInteract;
        return true;
    }

    public override bool PostPerform()
    {
        getRunning = false;
        //GetComponent<Guard>().distanceToInteract = previousDistanceToInteract;
        return true;
    }

    private void Update()
    {
        // Base code is not built to follow moving targets
        if (getRunning) 
            agent.SetDestination(GameObject.Find("Gorilla").transform.position);
    }
}
