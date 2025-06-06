using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/**
 * This class handles an agent's actionBelief so they are shared among actions
 * Each action used to have their own set of agentBeliefs, which are not shared among actions
 * Therefore, this class handles the agentBeliefs and makes them shared only among
 * the agent's actions. 
 */
public class GAgentBeliefHandler : MonoBehaviour
{
    private bool debugMode = false;
    public WorldStates beliefs = new WorldStates();
    private Dictionary<string, int> oldBeliefsDebug = new Dictionary<string, int>();

    public void EnableDebugMode()
    {
        debugMode = true;
        //print("Adding " + GWorld.DictionaryToString(beliefs.states));
        foreach (KeyValuePair<string, int> v in beliefs.states)
        {
            oldBeliefsDebug.Add(v.Key, v.Value); // make a static copy. Doing this = that will make this update
        }
    }

    private void Update()
    {
        if (debugMode)
        {
            foreach(KeyValuePair<string, int> v in beliefs.states)
            {
                if (oldBeliefsDebug.ContainsKey(v.Key))
                {
                    if (oldBeliefsDebug[v.Key] != v.Value)
                    {
                        print("DebugMode: Key <" + v.Key + "> value has changed to |" + v.Value + "|");
                        oldBeliefsDebug[v.Key] = v.Value;
                    }
                } else
                {
                    print("DebugMode: New key added <" + v.Key + ">:|" + v.Value + "|");
                    oldBeliefsDebug.Add(v.Key, v.Value);
                }
            }
        }
    }

}