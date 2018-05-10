using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VertFire : MonoBehaviour {

    private Animator animatorComp = null;
    private AudioSource fire;
    private AudioSource reload;
    private AudioSource[] sounds;

	// Use this for initialization
	void Start () {
        animatorComp = GetComponent<Animator>();
        sounds = GetComponents<AudioSource>();
        fire = sounds[0];
        reload = sounds[1];
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            fire.Play();
            animatorComp.SetTrigger("FireVert");
        } 
        else if (Input.GetKeyDown(KeyCode.R))
        {
            reload.Play();
            animatorComp.SetTrigger("ReloadVert");
        }
	}
}
