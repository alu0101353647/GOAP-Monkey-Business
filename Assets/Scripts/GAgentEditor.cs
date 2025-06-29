﻿using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

[CustomEditor(typeof(GAgentVisual))]
[CanEditMultipleObjects]
public class GAgentVisualEditor : Editor
{


    void OnEnable()
    {

    }

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        serializedObject.Update();
        GAgentVisual agent = (GAgentVisual)target;
        GUILayout.Label("Name: " + agent.name);
        GUILayout.Label("Current Action: " + agent.gameObject.GetComponent<GAgent>().currentAction);
        GUILayout.Label("Actions: ");
        foreach (GAction a in agent.gameObject.GetComponent<GAgent>().actions)
        {
            string pre = "";
            string eff = "";

            foreach (KeyValuePair<string, int> p in a.preconditions)
                pre += p.Key + ", " + p.Value;
            foreach (KeyValuePair<string, int> e in a.effects)
                eff += e.Key + ", " + e.Value;

            GUILayout.Label("====  " + a.actionName + "(" + pre + ")(" + eff + ")");
        }
        GUILayout.Label("Goals: ");
        foreach (KeyValuePair<SubGoal, int> g in agent.gameObject.GetComponent<GAgent>().goals)
        {
            GUILayout.Label("---: ");
            foreach (KeyValuePair<string, int> sg in g.Key.sgoals)
                GUILayout.Label("=====  " + sg.Key + "=" + sg.Value);
        }
        GUILayout.Label("Beliefs: ");
        foreach (KeyValuePair<string, int> sg in agent.gameObject.GetComponent<GAgent>().beliefs.GetStates())
        {
            GUILayout.Label("=====  " + sg.Key + "=" + sg.Value);
        }

        GUILayout.Label("World Beliefs: ");
        foreach (KeyValuePair<string, int> sg in GWorld.Instance.GetWorld().GetStates())
        {
            GUILayout.Label("=====  " + sg.Key + "=" + sg.Value);
        }


        GUILayout.Label("Inventory: ");
        foreach (GameObject g in agent.gameObject.GetComponent<GAgent>().inventory.items)
        {
            GUILayout.Label("====  " + g.tag);
        }


        serializedObject.ApplyModifiedProperties();
    }
}