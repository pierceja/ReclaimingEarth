using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectiveHandler : MonoBehaviour {

    [Header("Set in Inspector")]
    public Toggle greenCheckmark;
    public Toggle redCheckmark;
    public Toggle yellowCheckmark;
    public Toggle blueCheckmark;

    void Update()
    {
        // Check for completed objectives
        if (Objective.objectiveChanged)
        {
            ToggleCheckmark(Objective.objectiveName);
            Objective.objectiveName = "none";
            Objective.objectiveChanged = false;
        }
    }

    void ToggleCheckmark(string name)
    {
        switch (name)
        {
            case "GreenCylinder":
                greenCheckmark.isOn = true;
                break;
            case "RedCylinder":
                redCheckmark.isOn = true;
                break;
            case "YellowCylinder":
                yellowCheckmark.isOn = true;
                break;
            case "BlueCylinder":
                blueCheckmark.isOn = false;
                break;
            default:
                print("Error, name doesn't match any cylinder colors");
                break;
        }

    }
}
