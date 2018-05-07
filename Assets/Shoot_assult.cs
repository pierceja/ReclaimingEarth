using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    float bulletsInClip;
    float angleRadDown;
    float angleRadUp;
    Vector2 error;
    Quaternion errorRotation;
    AudioSource[] shoot;

    void Start()
    {
        angleRadDown = (float)(-accuracy / 180.0) * (float)Mathf.PI;
        angleRadUp = (float)(accuracy / 180.0) * (float)Mathf.PI;
        bulletsInClip = clipSize;
        Vector2 error = Random.insideUnitCircle * accuracy;
        Quaternion errorRotation = Quaternion.Euler(error.x, error.y, 0);
    }

    //Control three round burst
    int bulletsFired=0; //Stops firing when this equals three
    bool stopFiring=false;

    //Booleans to control which fire type is being used
    bool semiAuto = false;
    bool auto = true;
    bool burst;



    // Update is called once per frame
    void Update()
    {
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
        } else if (Input.GetKeyDown("r"))
        {
            if (bulletsInClip != clipSize)
            {
                bulletsInClip = clipSize;
                print("RELOADED");
                shoot[2].Play();
            }
        }

        if (Input.GetMouseButtonDown(0) && (bulletsInClip > 0))
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
                //print(bulletsInClip);


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
            shoot[1].Play();
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
        if (Input.GetButton("Fire1") && (bulletsInClip > 0))
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
                                                           transform.parent.rotation*errorRotation);
                    bulletsInClip--;
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
}
