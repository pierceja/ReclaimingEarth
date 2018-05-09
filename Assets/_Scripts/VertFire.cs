using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VertFire : MonoBehaviour {

    private Animator animatorComp = null;

	// Use this for initialization
	void Start () {
        animatorComp = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            // add a sound here if wanted
            animatorComp.SetTrigger("FireVert");
        }
		
	}
}
