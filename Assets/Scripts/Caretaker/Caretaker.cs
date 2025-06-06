using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Caretaker : GAgent
{
    [Tooltip("Minimum time that has to pass before the caretaker can be distracted")]
    [SerializeField] float minDistractionTime = 2;
    [Tooltip("Maximum time that can pass before the caretaker can be distracted")]
    [SerializeField] float maxDistractionTime = 7;

    private void Awake()
    {
        goals.Add(new SubGoal("fed", 0, false), 1);
        goals.Add(new SubGoal("distracted", 0, false), 2);

        Invoke("DistractSelf", Random.Range(minDistractionTime, maxDistractionTime));
    }

    void DistractSelf()
    {
        if (!beliefs.HasState("notificationReceived"))
        {
            beliefs.ModifyState("notificationReceived", 1);
        }
        Invoke("DistractSelf", Random.Range(minDistractionTime, maxDistractionTime));
    }
}
