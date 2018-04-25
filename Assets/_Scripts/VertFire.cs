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
        while (Input.GetKeyDown(KeyCode.Mouse0))
        {
            animatorComp.SetTrigger("FireVert");
        }
		
	}
}
