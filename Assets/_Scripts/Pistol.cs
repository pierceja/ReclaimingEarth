using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : MonoBehaviour
{

    private Animator animatorComp = null;
    private AudioSource fire1;
    private AudioSource fire2;
    private AudioSource fire3;
    private int fireSwitch = 0;
    private AudioSource reload;
    private AudioSource dryFire;
    private AudioSource[] sounds;
    private int clipSize = 15;
    private int fireInt;

    // Use this for initialization
    void Start()
    {
        animatorComp = GetComponent<Animator>();
        sounds = GetComponents<AudioSource>();

        fire1 = sounds[0];
        fire2 = sounds[1];
        fire3 = sounds[2];
        reload = sounds[3];
        dryFire = sounds[4];
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (clipSize > 0)
            {
                fireInt = Random.Range(1, 100);
                if (fireInt >= 1 && fireInt < 33)
                {
                    fire1.Play();
                } 
                else if (fireInt >= 33 && fireInt < 66)
                {
                    fire2.Play();
                }
                else
                {
                    fire3.Play();
                }
                animatorComp.SetTrigger("FirePistol");
                clipSize--;
            }
            else
            {
                dryFire.Play();
            }

        }
        else if (Input.GetKeyDown(KeyCode.R) && clipSize < 15)
        {
            reload.Play();
            animatorComp.SetTrigger("Reload");
            // need to wait two seconds before executing next line
            // can't fire while reloading
            clipSize = 15;
        }

    }
}
