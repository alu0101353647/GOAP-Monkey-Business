using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GACalmApe : GAction
{
    GameObject ape;
    public override bool PrePerform()
    {
        agentBeliefs.beliefs.SetState("idling", 0);
        //print("World states: " + GWorld.DictionaryToString(GWorld.Instance.GetWorld().states));
        return true;
    }

    public override bool PostPerform()
    {
        ape = GameObject.Find("Gorilla");
        agentBeliefs.beliefs.SetState("calmed", 1);
        ape.GetComponent<GGorilla>().enabled = false; // so it doesn't walk off
        return true;
    }
}
