using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using UnityEngine;

public class O_Node
{

    public Node parent;
    public float cost;
    public Dictionary<string, int> state;
    public GAction action;

    // Constructor
    public O_Node(Node parent, float cost, Dictionary<string, int> allStates, GAction action)
    {

        this.parent = parent;
        this.cost = cost;
        this.state = new Dictionary<string, int>(allStates);
        this.action = action;
    }
}

public class O_GPlanner
{
    // Merge two dictionaries, keeping the values of a over b
    Dictionary<string, int> DictionaryMerge(Dictionary<string, int> a, Dictionary<string, int> b)
    {
        foreach(KeyValuePair<string, int> content in b)
        {
            if (!a.ContainsKey(content.Key))
            {
                a.Add(content.Key, content.Value);
            }
        }
        return a;
    }

    public Queue<GAction> plan(List<GAction> actions, Dictionary<string, int> goal, WorldStates states)
    {

        // Check if goal's been achieved so far
        bool goalsAttained = true;
        //print("Goal: " + GWorld.DictionaryToString(goal));
        //print("States: " + GWorld.DictionaryToString(states.states));
        foreach (KeyValuePair<string, int> g in goal)
        {
            if (!states.states.ContainsKey(g.Key)) {
                goalsAttained = false;
                break;
            } else if (states.states[g.Key] != g.Value) {
                goalsAttained = false;
                break;
            }
        }

        if (goalsAttained) { /*print("Goals attained, idling");*/ return new(); } // No plan, goals have been achieved

        //print("New plan being planned-----------");

        List<GAction> usableActions = new List<GAction>();

        foreach (GAction a in actions)
        {
            if (a.IsAchievable())
            {
                usableActions.Add(a);
            }
        }

        List<Node> leaves = new List<Node>();
        // Modified to include the agent's beliefs
        Node start = new Node(null, 0.0f, DictionaryMerge(states.states, GWorld.Instance.GetWorld().GetStates()), null);

        //Debug.Log("The states are: ");
        //foreach (KeyValuePair<string, int> g in DictionaryMerge(states.states, GWorld.Instance.GetWorld().GetStates()))
        //{
        //    Debug.Log("Key: " + g.Key + ", Value: " + g.Value);
        //}

        //print("World states are: ");
        //print(GWorld.DictionaryToString(GWorld.Instance.GetWorld().GetStates()));

        //print("Personal beliefs:");
        //print(GWorld.DictionaryToString(states.states));

        //print("Actions stowed away are ");
        //foreach(GAction a in usableActions)
        //{
        //    print(a.actionName);
        //}

        //print("Actions in start " + GWorld.DictionaryToString(start.state));

        bool success = BuildGraph(start, leaves, usableActions, goal);

        if (!success)
        {
            //Debug.Log("NO PLAN");
            return null;
        }

        Node cheapest = null;
        foreach (Node leaf in leaves)
        {
            if (cheapest == null)
            {
                cheapest = leaf;
            }
            else if (leaf.cost < cheapest.cost)
            {
                cheapest = leaf;
            }
        }
        List<GAction> result = new List<GAction>();
        Node n = cheapest;

        while (n != null)
        {
            if (n.action != null)
            {
                result.Insert(0, n.action);
            }
            n = n.parent;
        }

        Queue<GAction> queue = new Queue<GAction>();

        foreach (GAction a in result)
        {
            queue.Enqueue(a);
        }

        //Debug.Log("The Plan is: ");
        //foreach (GAction a in queue)
        //{
        //    Debug.Log("Q: " + a.actionName);
        //}
        return queue;
    }

    private bool BuildGraph(Node parent, List<Node> leaves, List<GAction> usableActions, Dictionary<string, int> goal)
    {

        bool foundPath = false;
        //print("BuildGraph------------------");
        foreach (GAction action in usableActions)
        {
            //print(action.actionName + " in UsableActions---------");
            //print("Current state: " + GWorld.DictionaryToString(parent.state)); 
            if (action.IsAchievableGiven(parent.state))
            {
                //print("That action is achievable");
                Dictionary<string, int> currentState = new Dictionary<string, int>(parent.state);
                foreach (KeyValuePair<string, int> eff in action.effects)
                {
                    if (!currentState.ContainsKey(eff.Key))
                        currentState.Add(eff.Key, eff.Value);
                    // Modified to change effects depending on the new action
                    else
                        currentState[eff.Key] = eff.Value;
                }

                Node node = new Node(parent, parent.cost + action.cost, currentState, action);
                //print("Current state: " + GWorld.DictionaryToString(currentState));
                if (GoalAchieved(goal, currentState))
                {
                    leaves.Add(node);
                    foundPath = true;
                }
                else
                {
                    List<GAction> subset = ActionSubset(usableActions, action);
                    if (subset.Count == 0)
                    {
                        return false;
                    }
                    bool found = BuildGraph(node, leaves, subset, goal);
                    if (found)
                        foundPath = true;
                }
            }
        }
        return foundPath;
    }

    // Tired of typing "Debug.Log" every time I'm debugging.
    private void print(string s) { Debug.Log(s); }

    private List<GAction> ActionSubset(List<GAction> actions, GAction removeMe)
    {
        List<GAction> subset = new List<GAction>();
        foreach (GAction a in actions)
        {
            if (!a.Equals(removeMe))
                subset.Add(a);
        }
        return subset;
    }

    private bool GoalAchieved(Dictionary<string, int> goal, Dictionary<string, int> state)
    {
        //print("goal dict: ");
        //print(GWorld.DictionaryToString(goal));
        //print("state dict: ");
        //print(GWorld.DictionaryToString(goal));
        //print("--------------------");
        foreach (KeyValuePair<string, int> g in goal)
        {
            if (!state.ContainsKey(g.Key))
                return false;
            if (state[g.Key] != g.Value)
                return false;
        }
        return true;
    }
}
