using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// These are the shooting mechanics for the LMG. Bullets can randomly shoot off in different directions for added inaccuracy
/// </summary>

public class Shoot : MonoBehaviour
{
    [SerializeField] Transform firePosition;
    // bullet Prefab
    public GameObject bulletPrefab;
    public GameObject shootingPrefab;
    GameObject muzzleFlash;
    public float accuracy = 10;
    float elapsedTime;
    float fireRate = 10f;
    float lastFired;
    float angleRadDown;
    float angleRadUp;

    

    AudioSource shoot;

    void Start()
    {
        angleRadDown = (float)(-accuracy / 180.0) * (float)Mathf.PI;
        angleRadUp = (float)(accuracy / 180.0) * (float)Mathf.PI;
    }

    // Update is called once per frame
    void Update()
    {
        // left mouse clicked?
        if (Input.GetButton("Fire1"))
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

                GameObject g = (GameObject)Instantiate(bulletPrefab,
                                                       firePosition.position,
                                                       transform.parent.rotation);
                if (shoot == null)
                {
                    shoot = g.GetComponent<AudioSource>();
                    shoot.loop = false;
                }
                else
                {

                    shoot.Play();
                }


                // make the rocket fly forward by simply calling the rigidbody's
                // AddForce method
                // (requires the rocket to have a rigidbody attached to it)
                float force = g.GetComponent<Bullet>().speed;

                float shootAngle = Random.Range(0, 100);

                if (shootAngle <= 15.0)
                {
                    g.GetComponent<Rigidbody>().AddForce((Mathf.Sin(angleRadUp) * g.transform.up + Mathf.Cos(angleRadUp) * g.transform.forward) * force);
                }
                else if (shootAngle > 15.0 && shootAngle <= 30.0)
                {
                    g.GetComponent<Rigidbody>().AddForce((Mathf.Sin(angleRadDown) * g.transform.up + Mathf.Cos(angleRadDown) * g.transform.forward) * force);
                }
                else if (shootAngle > 30.0 && shootAngle <= 45.0)
                {
                    g.GetComponent<Rigidbody>().AddForce((Mathf.Sin(angleRadUp) * g.transform.right + Mathf.Cos(angleRadUp) * g.transform.forward) * force);
                }
                else if (shootAngle > 45.0 && shootAngle <= 60.0)
                {
                    g.GetComponent<Rigidbody>().AddForce((Mathf.Sin(angleRadDown) * g.transform.right + Mathf.Cos(angleRadDown) * g.transform.forward) * force);
                }
                else
                {
                    g.GetComponent<Rigidbody>().AddForce(g.transform.forward * force);
                }




            }


        }
        else
        {
            //Remove muzzleflash if not firing
            Destroy(muzzleFlash);
            if (shoot != null)
            {
                shoot.Stop();
            }
        }
    }
}