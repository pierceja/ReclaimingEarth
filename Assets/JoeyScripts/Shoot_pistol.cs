using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot_pistol : MonoBehaviour {

    [SerializeField] Transform firePosition;
    // bullet Prefab
    public GameObject bulletPrefab;
    public GameObject shootingPrefab;
    GameObject muzzleFlash;

    float angleRadDown = (float)(-10 / 180.0) * (float)Mathf.PI;
    float angleRadUp = (float)(10 / 180.0) * (float)Mathf.PI;

    AudioSource shoot;




    // Update is called once per frame
    void Update()
    {
        // left mouse clicked?
        if (Input.GetMouseButtonDown(0))
        {
            muzzleFlash = Instantiate(shootingPrefab,
                        firePosition.position,
                        Quaternion.identity);


            //Update position and rotation with firePosition
            muzzleFlash.transform.position = firePosition.position;
            muzzleFlash.transform.rotation = firePosition.rotation;

            GameObject bullet1 = (GameObject)Instantiate(bulletPrefab,
                                                   firePosition.position,
                                                  transform.parent.rotation);


            if (shoot == null)
            {
                shoot = bullet1.GetComponent<AudioSource>();
                shoot.loop = false;
            }
            else
            {

                shoot.Play();
            }


            // make the bullet fly forward by simply calling the rigidbody's
            // AddForce method
            // Fires four bullets in four different directions for burst shot
            float force = bullet1.GetComponent<Bullet>().speed;

            bullet1.GetComponent<Rigidbody>().GetComponent<Rigidbody>().AddForce(bullet1.transform.forward * force);

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
