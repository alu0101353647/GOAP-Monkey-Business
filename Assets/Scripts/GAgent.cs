﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SubGoal
{
    public Dictionary<string, int> sgoals;
    public bool remove;

    public SubGoal(string s, int i, bool r)
    {
        sgoals = new Dictionary<string, int>();
        sgoals.Add(s, i);
        remove = r;
    }
}

public class GAgent : MonoBehaviour
{
    public List<GAction> actions = new List<GAction>();
    public Dictionary<SubGoal, int> goals = new Dictionary<SubGoal, int>();
    public WorldStates beliefs = new WorldStates();
    public GInventory inventory = new GInventory();

    protected GPlanner planner;
    Queue<GAction> actionQueue;
    public GAction currentAction;
    SubGoal currentGoal;

    [Tooltip("Whether or not use NavMesh's RemainingDistance method to check remaining distance")]
    public bool useNavMeshRemainingDistance = true;
    public float distanceToInteract = 1f;

    public bool debugMode = false;

    // Start is called before the first frame update
    public void Start()
    {
        GAction[] acts = this.GetComponents<GAction>();
        foreach (GAction a in acts)
            actions.Add(a);
    }


    bool invoked = false;
    void CompleteAction()
    {
        currentAction.running = false;
        currentAction.PostPerform();
        invoked = false;
    }

    public void ResetPlan()
    {
        planner = null;
        currentAction = null;
    }

    void LateUpdate()
    {
        if (currentAction != null && currentAction.running)
        {
            // si el navmesh no está calculando bien el remaining distance, se puede
            //calcular la distancia a mano.
            float distanceToTarget = 0f;
            if (useNavMeshRemainingDistance)
            {
                distanceToTarget = currentAction.agent.remainingDistance;
            } else
            {
                distanceToTarget = Vector3.Distance(currentAction.target.transform.position, this.transform.position);
            }
            if (currentAction.agent.hasPath && distanceToTarget < distanceToInteract)
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
            planner.debugMode = debugMode;

            var sortedGoals = from entry in goals orderby entry.Value descending select entry;

            foreach (KeyValuePair<SubGoal, int> sg in sortedGoals)
            {
                actionQueue = planner.plan(actions, sg.Key.sgoals, beliefs);
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
