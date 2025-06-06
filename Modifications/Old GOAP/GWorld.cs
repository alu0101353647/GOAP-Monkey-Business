using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class O_GWorld
{
    private static readonly GWorld instance = new GWorld();
    private static WorldStates world;

    static O_GWorld()
    {
        world = new WorldStates();
    }

    private O_GWorld()
    {
    }

    public static GWorld Instance
    {
        get { return instance; }
    }

    public WorldStates GetWorld()
    {
        return world;
    }

    // Utility function for debugging purposes available to everyone
    public static string DictionaryToString(Dictionary<string, int> dict)
    {
        string result = "|";
        foreach (KeyValuePair<string, int> v in dict)
        {
            result += v.Key + ":" + v.Value + "|";
        }
        if (result == "|") { result = "|EMPTY|"; }
        return result;
    }
}
