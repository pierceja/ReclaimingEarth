using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Objective : MonoBehaviour {

    static public bool objectiveChanged = false;
    static public string objectiveName = "none";

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            objectiveName = name;
            objectiveChanged = true;
            gameObject.active = false;
        }
    }
}
