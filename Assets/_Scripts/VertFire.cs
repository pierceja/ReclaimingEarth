using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VertFire : MonoBehaviour {

    private Animator animatorComp = null;
    private AudioSource fire;
    private AudioSource reload;
    private AudioSource dryFire;
    private AudioSource[] sounds;
    private int clipSize = 30;

	// Use this for initialization
	void Start () {
        animatorComp = GetComponent<Animator>();
        sounds = GetComponents<AudioSource>();
        fire = sounds[0];
        reload = sounds[1];
        dryFire = sounds[2];
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (clipSize > 0)
            {
                fire.Play();
                animatorComp.SetTrigger("FireVert");
                clipSize--;
            }
            else
            {
                dryFire.Play();
            }
            
        } 
        else if (Input.GetKeyDown(KeyCode.R))
        {
            reload.Play();
            animatorComp.SetTrigger("ReloadVert");
            // need to wait two seconds before executing next line
            // can't fire while reloading
            clipSize = 30;
        }
	}
}
