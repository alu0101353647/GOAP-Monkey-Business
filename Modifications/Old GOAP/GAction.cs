using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class O_GAction : MonoBehaviour
{
    public string actionName = "Action";
    public float cost = 1.0f;
    public GameObject target;
    public string targetTag;
    public float duration = 0;
    public WorldState[] preConditions;
    public WorldState[] afterEffects;
    public NavMeshAgent agent;

    public Dictionary<string, int> preconditions;
    public Dictionary<string, int> effects;

    protected GAgentBeliefHandler agentBeliefs;

    public bool running = false;

    public O_GAction()
    {
        preconditions = new Dictionary<string, int>();
        effects = new Dictionary<string, int>();
    }

    public void Awake()
    {
        agent = this.gameObject.GetComponent<NavMeshAgent>();
        agentBeliefs = gameObject.GetComponent<GAgentBeliefHandler>();
        if (agentBeliefs == null)
        {
            Debug.LogError("You forgot to add GAgentBeliefHandler to the GO");
        }
        if (preConditions != null)
            foreach (WorldState w in preConditions)
            {
                preconditions.Add(w.key, w.value);
            }

        if (afterEffects != null)
            foreach (WorldState w in afterEffects)
            {
                effects.Add(w.key, w.value);
            }

    }

    public bool IsAchievable()
    {
        return true;
    }

    // Modification: instead of just checking whether the belief is in there
    // it also checks the value
    public bool IsAchievableGiven(Dictionary<string, int> conditions)
    {
        //print("-----------");
        //print("Every condition in param: ");
        //print(GWorld.DictionaryToString(conditions));
        //print("Every precondition: ");
        //print(GWorld.DictionaryToString(preconditions));
        foreach (KeyValuePair<string, int> p in preconditions)
        {
            if (!conditions.ContainsKey(p.Key))
            {
                //print("Conditions doesn't include the key " + p.Key);
                return false;
            } else
            {
                if (p.Value != conditions[p.Key])
                {
                    //print("Conditions value " + conditions[p.Key] + " is not the same as " + p.Key + ":" + p.Value);
                    return false;
                }
            }
        }
        //print("Is achievable");
        return true;
    }

    public abstract bool PrePerform();
    public abstract bool PostPerform();
}
