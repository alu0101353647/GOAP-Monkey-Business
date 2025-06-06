using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class O_SubGoal
{
    public Dictionary<string, int> sgoals;
    public bool remove;

    public O_SubGoal(string s, int i, bool r)
    {
        sgoals = new Dictionary<string, int>();
        sgoals.Add(s, i);
        remove = r;
    }

    public override string ToString()
    {
        string result = "|";
        foreach (KeyValuePair<string, int> pair in sgoals)
        {
            result += pair.Key + ":" + pair.Value + "|";
        }
        if (result == "|") { result = "|EMPTY|"; }
        return result;
    }
}

public class O_GAgent : MonoBehaviour
{
    public List<GAction> actions = new List<GAction>();
    public Dictionary<SubGoal, int> goals = new Dictionary<SubGoal, int>();
    protected GAgentBeliefHandler agentBeliefs;

    GPlanner planner;
    Queue<GAction> actionQueue;
    public GAction currentAction;
    SubGoal currentGoal;

    [Tooltip("Set to true if you want to be updated on the beliefs in the console")]
    [SerializeField] private bool debugMode = false;

    [SerializeField] private float distanceToInteract = 1f;

    // Start is called before the first frame update
    public void Start()
    {
        GAction[] acts = this.GetComponents<GAction>();
        foreach (GAction a in acts)
            actions.Add(a);
        agentBeliefs = gameObject.GetComponent<GAgentBeliefHandler>();
        if (debugMode) { agentBeliefs.EnableDebugMode(); }
    }

    bool invoked = false;
    void CompleteAction()
    {
        currentAction.running = false;
        currentAction.PostPerform();
        invoked = false;
    }

    void LateUpdate()
    {
        if (currentAction != null && currentAction.running)
        {
            if (currentAction.agent.hasPath && currentAction.agent.remainingDistance < distanceToInteract)
            {
                if (!invoked)
                {
                    Invoke("CompleteAction", currentAction.duration);
                    invoked = true;
                }
            }
            return;
        }

        if (planner == null || actionQueue == null)
        {
            planner = new GPlanner();

            var sortedGoals = from entry in goals orderby entry.Value descending select entry;

            //Debug.Log("Sortes goals: ");
            foreach (KeyValuePair<SubGoal, int> sg in sortedGoals)
            {
                //Debug.Log("SubGoal: " + sg.Key.ToString() + ", Int: " + sg.Value);
                //Debug.Log("Actions are:");
                //foreach (GAction ac in actions)
                //{
                //    print(ac.actionName);
                //}
                //print("Agent beliefs: " + GWorld.DictionaryToString(agentBeliefs.beliefs.states));
                actionQueue = planner.plan(actions, sg.Key.sgoals, agentBeliefs.beliefs);
                if (actionQueue != null)
                {
                    currentGoal = sg.Key;
                    break;
                }
            }
        }

        if (actionQueue != null && actionQueue.Count == 0)
        {
            if (currentGoal.remove)
            {
                goals.Remove(currentGoal);
            }
            //currentAction = idle;
            planner = null;
        }

        if (actionQueue != null && actionQueue.Count > 0)
        {
            currentAction = actionQueue.Dequeue();
            if (currentAction.PrePerform())
            {
                if (currentAction.target == null && currentAction.targetTag != "")
                    currentAction.target = GameObject.FindWithTag(currentAction.targetTag);

                if (currentAction.target != null)
                {
                    currentAction.running = true;
                    currentAction.agent.SetDestination(currentAction.target.transform.position);
                }
            }
            else
            {
                actionQueue = null;
            }

        }

    }
}
