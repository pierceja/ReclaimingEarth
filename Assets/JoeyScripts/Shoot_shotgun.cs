using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot_shotgun : MonoBehaviour
{

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

            GameObject bullet2 = (GameObject)Instantiate(bulletPrefab,
                                                   firePosition.position,
                                                   transform.parent.rotation);

            GameObject bullet3 = (GameObject)Instantiate(bulletPrefab,
                                                   firePosition.position,
                                                   transform.parent.rotation);

            GameObject bullet4 = (GameObject)Instantiate(bulletPrefab,
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
            float force = bullet1.GetComponent<Slug>().speed;

            bullet1.GetComponent<Rigidbody>().AddForce((Mathf.Sin(angleRadUp) * bullet1.transform.up + Mathf.Cos(angleRadUp) * bullet1.transform.forward) * force);

            bullet2.GetComponent<Rigidbody>().AddForce((Mathf.Sin(angleRadDown) * bullet2.transform.up + Mathf.Cos(angleRadDown) * bullet2.transform.forward) * force);


            bullet3.GetComponent<Rigidbody>().AddForce((Mathf.Sin(angleRadUp) * bullet3.transform.right + Mathf.Cos(angleRadUp) * bullet3.transform.forward) * force);


            bullet4.GetComponent<Rigidbody>().AddForce((Mathf.Sin(angleRadDown) * bullet4.transform.right + Mathf.Cos(angleRadDown) * bullet4.transform.forward) * force);






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
