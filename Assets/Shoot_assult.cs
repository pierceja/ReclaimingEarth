using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shoot_assult : MonoBehaviour {

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
    public float clipSize = 60;
    public float totalAmmo = 100;
    float bulletsInClip;
    float angleRadDown;
    float angleRadUp;
    Vector2 error;
    Quaternion errorRotation;
    AudioSource[] shoot;
    float reloadTime;

    private Animator animatorComp = null;
    private AudioSource fire;
    private AudioSource reload;
    private AudioSource dryFire;
    private AudioSource[] sounds;


    void Start()
    {
        animatorComp = GetComponent<Animator>();
        sounds = GetComponents<AudioSource>();
        fire = sounds[0];
        reload = sounds[1];
        dryFire = sounds[2];

        angleRadDown = (float)(-accuracy / 180.0) * (float)Mathf.PI;
        angleRadUp = (float)(accuracy / 180.0) * (float)Mathf.PI;
        bulletsInClip = clipSize;
        Vector2 error = Random.insideUnitCircle * accuracy;
        Quaternion errorRotation = Quaternion.Euler(error.x, error.y, 0);

        // Show starting ammo
        DisplayAmmo(bulletsInClip, totalAmmo);

        reloadTime = 130;
    }

    //Control three round burst
    int bulletsFired=0; //Stops firing when this equals three
    bool stopFiring=false;

    //Booleans to control which fire type is being used
    bool semiAuto = false;
    bool auto = true;
    bool burst;

    void OnEnable()
    {
        // Show current ammo
        DisplayAmmo(bulletsInClip, totalAmmo);
    }

    void FixedUpdate()
    {
        reloadTime += 1;
    }

    // Update is called once per frame
    void Update()
    {

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
        } else if (Input.GetKeyDown("3"))
        {
            auto = false;
            semiAuto = true;
            burst = false;
        } else if (Input.GetKeyDown("r") && bulletsInClip < clipSize && totalAmmo != 0)
        {
            if (bulletsInClip != clipSize)
            {
                reloadTime = 0;

                // This line causes reload not to work
                //animatorComp.SetTrigger("ReloadVert");

                // Always reload full clipsize in. If total ammo is negative, still add negative value into the bulletsInClip
                float bulletsShot = clipSize - bulletsInClip;
                bulletsInClip = clipSize;
                totalAmmo -= bulletsShot;
                if (totalAmmo < 0)
                {
                    bulletsInClip = totalAmmo + clipSize;
                    totalAmmo = 0;
                }
                shoot[1].Play();
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
                    muzzleFlash = PhotonNetwork.Instantiate("MuzzleFlash",
                            firePosition.position,
                            Quaternion.identity, 0);
                }

                //Update position and rotation with firePosition
                muzzleFlash.transform.position = firePosition.position;
                muzzleFlash.transform.rotation = firePosition.rotation;

                error = Random.insideUnitCircle * accuracy;
                errorRotation = Quaternion.Euler(error.x, error.y, 0);
                GameObject g = PhotonNetwork.Instantiate("Assultbullet",
                                                       firePosition.position,
                                                       transform.parent.rotation * errorRotation, 0);
                bulletsInClip--;
                // Display ammo change on HUD
                DisplayAmmo(bulletsInClip, totalAmmo);
                //print(bulletsInClip);

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
                //if (shoot == null)
                //{
                //    shoot = GetComponents<AudioSource>();
                //    shoot[0].loop = false;
                //}
                //else
                //{

                //    shoot[0].Play();
                //}


                // make the rocket fly forward by simply calling the rigidbody's
                // AddForce method
                // (requires the rocket to have a rigidbody attached to it)
                float force = g.GetComponent<Bullet>().speed;
                g.GetComponent<Rigidbody>().AddForce(g.transform.forward * force);

                //    float shootAngle = Random.Range(0, 100);

                //    if (shootAngle <= 15.0)
                //    {
                //        g.GetComponent<Rigidbody>().AddForce((Mathf.Sin(angleRadUp) * g.transform.up + Mathf.Cos(angleRadUp) * g.transform.forward) * force);
                //    }
                //    else if (shootAngle > 15.0 && shootAngle <= 30.0)
                //    {
                //        g.GetComponent<Rigidbody>().AddForce((Mathf.Sin(angleRadDown) * g.transform.up + Mathf.Cos(angleRadDown) * g.transform.forward) * force);
                //    }
                //    else if (shootAngle > 30.0 && shootAngle <= 45.0)
                //    {
                //        g.GetComponent<Rigidbody>().AddForce((Mathf.Sin(angleRadUp) * g.transform.right + Mathf.Cos(angleRadUp) * g.transform.forward) * force);
                //    }
                //    else if (shootAngle > 45.0 && shootAngle <= 60.0)
                //    {
                //        g.GetComponent<Rigidbody>().AddForce((Mathf.Sin(angleRadDown) * g.transform.right + Mathf.Cos(angleRadDown) * g.transform.forward) * force);
                //    }
                //    else
                //    {
                //        g.GetComponent<Rigidbody>().AddForce(g.transform.forward * force);
                //    }

            }
        }
        else if (Input.GetMouseButtonDown(0) && (bulletsInClip <= 0))
        {
            shoot[2].Play();
        }
        else
        {
            //Remove muzzleflash if not firing
            PhotonNetwork.Destroy(muzzleFlash);
            // if (shoot != null)
            //   {
            //     shoot.Stop();
            // }
        }

        // left mouse clicked?
        if (Input.GetButton("Fire1") && (bulletsInClip > 0) && reloadTime > 120)
        {
            if (burst && (bulletsFired >=3))
            {
                bulletsFired = 0;
            }

            if ((auto || burst) && !stopFiring) {
                if (Time.time - lastFired > 1 / fireRate)
                {

                    lastFired = Time.time;
                    if (muzzleFlash == null)
                    {
                        muzzleFlash = PhotonNetwork.Instantiate("MuzzleFlash",
                                firePosition.position,
                                Quaternion.identity, 0);
                    }

                    //Update position and rotation with firePosition
                    muzzleFlash.transform.position = firePosition.position;
                    muzzleFlash.transform.rotation = firePosition.rotation;

                    error = Random.insideUnitCircle * accuracy;
                    errorRotation = Quaternion.Euler(error.x, error.y, 0);
                    GameObject g = PhotonNetwork.Instantiate("Assultbullet",
                                                           firePosition.position,
                                                           transform.parent.rotation*errorRotation, 0);
                    bulletsInClip--;
                    // Display ammo change on HUD
                    DisplayAmmo(bulletsInClip, totalAmmo);
                    bulletsFired++;
                    //print(bulletsInClip);
                    //print(bulletsFired);
                    if ((bulletsFired >= 3) && burst)
                    {
                        stopFiring = true;
                        //print("Hello");
                    }

                    if (shoot == null)
                    {
                        shoot = GetComponents<AudioSource>();
                        shoot[0].loop = false;
                    }
                    else
                    {

                        shoot[0].Play();
                    }


                    // make the rocket fly forward by simply calling the rigidbody's
                    // AddForce method
                    // (requires the rocket to have a rigidbody attached to it)
                    float force = g.GetComponent<Bullet>().speed;
                    g.GetComponent<Rigidbody>().AddForce(g.transform.forward * force);

                    //float shootAngle = Random.Range(0, 100);

                    //if (shootAngle <= 15.0)
                    //{
                    //    g.GetComponent<Rigidbody>().AddForce((Mathf.Sin(angleRadUp) * g.transform.up + Mathf.Cos(angleRadUp) * g.transform.forward) * force);
                    //}
                    //else if (shootAngle > 15.0 && shootAngle <= 30.0)
                    //{
                    //    g.GetComponent<Rigidbody>().AddForce((Mathf.Sin(angleRadDown) * g.transform.up + Mathf.Cos(angleRadDown) * g.transform.forward) * force);
                    //}
                    //else if (shootAngle > 30.0 && shootAngle <= 45.0)
                    //{
                    //    g.GetComponent<Rigidbody>().AddForce((Mathf.Sin(angleRadUp) * g.transform.right + Mathf.Cos(angleRadUp) * g.transform.forward) * force);
                    //}
                    //else if (shootAngle > 45.0 && shootAngle <= 60.0)
                    //{
                    //    g.GetComponent<Rigidbody>().AddForce((Mathf.Sin(angleRadDown) * g.transform.right + Mathf.Cos(angleRadDown) * g.transform.forward) * force);
                    //}
                    //else
                    //{
                    //    g.GetComponent<Rigidbody>().AddForce(g.transform.forward * force);
                    //}

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
    void DisplayAmmo(float bulletsInClipVar, float totalAmmoVar)
    {
        Text magAmmoUI = GameObject.Find("/Player HUD/WeaponUI/MagAmmo").GetComponent<Text>();
        Text totalAmmoUI = GameObject.Find("/Player HUD/WeaponUI/TotalAmmo").GetComponent<Text>();

        if (magAmmoUI != null)
            magAmmoUI.text = bulletsInClipVar.ToString();
        if (totalAmmoUI != null)
            totalAmmoUI.text = totalAmmoVar.ToString();
    }
}
