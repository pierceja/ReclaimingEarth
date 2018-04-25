using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectiveHandler : MonoBehaviour {

    [Header("Set in Inspector")]
    public GameObject greenCheckmark;
    public GameObject redCheckmark;
    public GameObject yellowCheckmark;
    public GameObject blueCheckmark;
    public GameObject[] objectives;

    void Update()
    {
        // Check for completed objectives
        if (Objective.objectiveChanged)
        {

            Objective.objectiveChanged = false;
        }
    }
}
