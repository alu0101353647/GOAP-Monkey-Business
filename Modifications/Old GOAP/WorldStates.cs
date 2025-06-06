using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class O_WorldState
{
    public string key;
    public int value;
}

public class O_WorldStates
{
    public Dictionary<string, int> states;

    public O_WorldStates()
    {
        states = new Dictionary<string, int>();
    }

    public bool HasState(string key)
    {
        return states.ContainsKey(key);
    }

    void AddState(string key, int value)
    {
        states.Add(key, value);
    }

    public void ModifyState(string key, int value)
    {
        if (states.ContainsKey(key))
        {
            states[key] += value;
            if (states[key] <= 0)
                RemoveState(key);
        }
        else
            states.Add(key, value);
    }

    public void RemoveState(string key)
    {
        if (states.ContainsKey(key))
            states.Remove(key);
    }

    public void SetState(string key, int value)
    {
        if (states.ContainsKey(key))
            states[key] = value;
        else
            states.Add(key, value);
    }

    public Dictionary<string, int> GetStates()
    {
        return states;
    }

    public int GetStateValue(string key)
    {
        if (states.ContainsKey(key))
        {
            return states[key];
        } else
        {
            Debug.LogError("Tried to get state " + key + ", which doesn't exist");
            return -404;
        }
    }

    public override string ToString()
    {
        string result = "|";
        foreach (KeyValuePair<string, int> s in states)
        {
            result += s.Key + ":" + s.Value + "|";
        }
        if (result == "|") { result = "|EMPTY|"; }
        return result;
    }
}
