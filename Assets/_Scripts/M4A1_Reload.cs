using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M4A1_Reload : MonoBehaviour {
    
    // Update is called once per frame
	void Update () {
		if (Input.GetButtonDown("r"))
        {
            AudioSource reloadSound = GetComponent<AudioSource>();
            reloadSound.Play();
            GetComponent<Animation>().Play("M4A1_BetterReload");
        }
	}
}
