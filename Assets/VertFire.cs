using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VertFire : MonoBehaviour {

    [SerializeField] Transform firePosition;
    // bullet Prefab
    public GameObject bulletPrefab;
    public GameObject shootingPrefab;
    GameObject muzzleFlash;
    public float accuracy = 10;
    float elapsedTime;
    public float fireRate = 10f;
    float lastFired;
    //Handle clip size
    public int clipSize = 60;
    public int totalAmmo = 100;
    int bulletsInClip;
    float angleRadDown;
    float angleRadUp;
    Vector2 error;
    Quaternion errorRotation;
    float reloadTime;

    private Animator animatorComp = null;
    private AudioSource fire;
    private AudioSource reload;
    private AudioSource dryFire;
    private AudioSource[] sounds;

	// Use this for initialization
	void Start () {
        animatorComp = GetComponent<Animator>();
        sounds = GetComponents<AudioSource>();
        fire = sounds[0];
        reload = sounds[1];
        dryFire = sounds[2];

        angleRadDown = (float)(-accuracy / 180.0) * (float)Mathf.PI;
        angleRadUp = (float)(accuracy / 180.0) * (float)Mathf.PI;
        bulletsInClip = clipSize;
        // Show starting ammo
        DisplayAmmo(bulletsInClip, totalAmmo);
        Vector2 error = Random.insideUnitCircle * accuracy;
        Quaternion errorRotation = Quaternion.Euler(error.x, error.y, 0);

        reloadTime = 130;
    }

    //Control three round burst
    int bulletsFired = 0; //Stops firing when this equals three
    bool stopFiring = false;

    //Booleans to control which fire type is being used
    bool semiAuto = false;
    bool auto = true;
    bool burst = false;


    void Update()
    {
        reloadTime+=1;

        // Display ammo change on HUD after reloading is finished
        if (reloadTime == 120)
            DisplayAmmo(bulletsInClip, totalAmmo);

        //Firing mode can change with keyboard presses
        if (Input.GetKeyDown("1"))
        {
            auto = true;
            semiAuto = false;
            burst = false;
        }
        else if (Input.GetKeyDown("2"))
        {
            auto = false;
            semiAuto = false;
            burst = true;
        }
        else if (Input.GetKeyDown("3"))
        {
            auto = false;
            semiAuto = true;
            burst = false;
        }
        else if (Input.GetKeyDown("r") && bulletsInClip < clipSize && totalAmmo != 0)
        {
            if (bulletsInClip != clipSize)
            {
                reloadTime = 0;
                animatorComp.SetTrigger("ReloadVert");
                sounds[1].Play();

                // Always reload full clipsize in. If total ammo is negative, still add negative value into the bulletsInClip
                int bulletsShot = clipSize - bulletsInClip;
                bulletsInClip = clipSize;
                totalAmmo -= bulletsShot;
                if (totalAmmo < 0)
                {
                    bulletsInClip = totalAmmo + clipSize;
                    totalAmmo = 0;
                }

            }
        }

        if (Input.GetMouseButtonDown(0) && (bulletsInClip > 0) && reloadTime > 120)
        {
            if (stopFiring)
            {
                stopFiring = false;
            }

            if (semiAuto)
            {
                if (muzzleFlash == null)
                {
                    muzzleFlash = Instantiate(shootingPrefab,
                            firePosition.position,
                            Quaternion.identity);
                }

                //Update position and rotation with firePosition
                muzzleFlash.transform.position = firePosition.position;
                muzzleFlash.transform.rotation = firePosition.rotation;

                error = Random.insideUnitCircle * accuracy;
                errorRotation = Quaternion.Euler(error.x, error.y, 0);
                GameObject g = (GameObject)Instantiate(bulletPrefab,
                                                       firePosition.position,
                                                       transform.parent.rotation * errorRotation);
                bulletsInClip--;
                // Display ammo change on HUD
                DisplayAmmo(bulletsInClip, totalAmmo);
                print(bulletsInClip);


                if (sounds == null)
                {
                    sounds = GetComponents<AudioSource>();
                    sounds[0].loop = false;
                }
                else
                {
                    animatorComp.SetTrigger("FireVert");
                    sounds[0].Play();
                }


                // make the rocket fly forward by simply calling the rigidbody's
                // AddForce method
                // (requires the rocket to have a rigidbody attached to it)
                float force = g.GetComponent<Bullet>().speed;
                g.GetComponent<Rigidbody>().AddForce(g.transform.forward * force);


            }
        }
        else if (Input.GetMouseButtonDown(0) && (bulletsInClip <= 0))
        {
            sounds[2].Play();
        }
        else
        {
            //Remove muzzleflash if not firing
            Destroy(muzzleFlash);
            // if (shoot != null)
            //   {
            //     shoot.Stop();
            // }
        }

        // left mouse clicked?
        if (Input.GetButton("Fire1") && (bulletsInClip > 0) && reloadTime > 120)
        {
            if (burst && (bulletsFired >= 3))
            {
                bulletsFired = 0;
            }

            if ((auto || burst) && !stopFiring)
            {
                if (Time.time - lastFired > 1 / fireRate)
                {

                    lastFired = Time.time;
                    if (muzzleFlash == null)
                    {
                        muzzleFlash = Instantiate(shootingPrefab,
                                firePosition.position,
                                Quaternion.identity);
                    }

                    //Update position and rotation with firePosition
                    muzzleFlash.transform.position = firePosition.position;
                    muzzleFlash.transform.rotation = firePosition.rotation;

                    error = Random.insideUnitCircle * accuracy;
                    errorRotation = Quaternion.Euler(error.x, error.y, 0);
                    GameObject g = (GameObject)Instantiate(bulletPrefab,
                                                           firePosition.position,
                                                           transform.parent.rotation * errorRotation);
                    bulletsInClip--;
                    // Display ammo change on HUD
                    DisplayAmmo(bulletsInClip, totalAmmo);
                    bulletsFired++;
                    print(bulletsInClip);
                    //print(bulletsFired);
                    if ((bulletsFired >= 3) && burst)
                    {
                        stopFiring = true;
                        //print("Hello");
                    }

                    if (sounds == null)
                    {
                        sounds = GetComponents<AudioSource>();
                        sounds[0].loop = false;
                    }
                    else
                    {
                        animatorComp.SetTrigger("FireVert");
                        sounds[0].Play();
                    }


                    // make the rocket fly forward by simply calling the rigidbody's
                    // AddForce method
                    // (requires the rocket to have a rigidbody attached to it)
                    float force = g.GetComponent<Bullet>().speed;
                    g.GetComponent<Rigidbody>().AddForce(g.transform.forward * force);

                    
                }


            }


        }
        else
        {
            //Remove muzzleflash if not firing
            Destroy(muzzleFlash);
            //  if (shoot != null)
            //  {
            //     shoot.Stop();
            // }
        }
    }

    // Display the ammo amounts on the HUD
    void DisplayAmmo(int bulletsInClipVar, int totalAmmoVar)
    {
        Text magAmmoUI = GameObject.Find("/Player HUD/WeaponUI/MagAmmo").GetComponent<Text>();
        print("Mag ammo:" + magAmmoUI.text);
        Text totalAmmoUI = GameObject.Find("/Player HUD/WeaponUI/TotalAmmo").GetComponent<Text>();
        print("Total ammo: " + totalAmmoUI.text);

        if (magAmmoUI != null)
            magAmmoUI.text = bulletsInClipVar.ToString();
        if (totalAmmoUI != null)
            totalAmmoUI.text = totalAmmoVar.ToString();
    }
}








   

