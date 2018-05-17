using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    private int fireInt;

    [SerializeField] Transform firePosition;
    // bullet Prefab
    public GameObject bulletPrefab;
    public GameObject shootingPrefab;
    public float accuracy = 10;
    GameObject muzzleFlash;
    //Handle clip size
    public float clipSize = 24;
    public float totalAmmo = 100;
    float bulletsInClip;
    float angleRadDown;
    float angleRadUp;
    Vector2 error;
    Quaternion errorRotation;
    AudioSource[] shoot;
    float reloadTime;

    void Start()
    {
        //animatorComp = GetComponent<Animator>();
        //    //sounds = GetComponents<AudioSource>();

        //    //fire1 = sounds[0];
        //    //fire2 = sounds[1];
        //    //fire3 = sounds[2];
        //    //reload = sounds[3];
        //    //dryFire = sounds[4];

        angleRadDown = (float)(-accuracy / 180.0) * (float)Mathf.PI;
        angleRadUp = (float)(accuracy / 180.0) * (float)Mathf.PI;
        bulletsInClip = clipSize;
        Vector2 error = Random.insideUnitCircle * accuracy;
        Quaternion errorRotation = Quaternion.Euler(error.x, error.y, 0);

        // Show starting ammo
        DisplayAmmo(bulletsInClip, totalAmmo);
        reloadTime = 90;
    }

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
        if (reloadTime == 90)
            DisplayAmmo(bulletsInClip, totalAmmo);

        if (Input.GetKeyDown(KeyCode.Mouse0) && reloadTime > 90)
        {
            if (bulletsInClip > 0)
            {
                muzzleFlash = PhotonNetwork.Instantiate("MuzzleFlash",
                        firePosition.position,
                        Quaternion.identity, 0);

                muzzleFlash.transform.position = firePosition.position;
                muzzleFlash.transform.rotation = firePosition.rotation;
                error = Random.insideUnitCircle * accuracy;
                errorRotation = Quaternion.Euler(error.x, error.y, 0);
                GameObject g = (GameObject)PhotonNetwork.Instantiate("Assultbullet",
                                                       firePosition.position,
                                                      transform.parent.rotation * errorRotation, 0);

                bulletsInClip--;
                DisplayAmmo(bulletsInClip, totalAmmo);

                //sounds[0].Play();
                //animatorComp.SetTrigger("FirePistol");

                float force = g.GetComponent<Bullet>().speed;
                float shootAngle = Random.Range(0, 100);
                g.GetComponent<Rigidbody>().AddForce(g.transform.forward * force);
            }
            else
            {
                //sounds[1].Play();
            }

        }
        else if (Input.GetKeyDown(KeyCode.R) && bulletsInClip < clipSize && totalAmmo != 0)
        {
            if (bulletsInClip != clipSize)
            {
                reloadTime = 0;

                // Always reload full clipsize in. If total ammo is negative, still add negative value into the bulletsInClip
                float bulletsShot = clipSize - bulletsInClip;
                bulletsInClip = clipSize;
                totalAmmo -= bulletsShot;
                if (totalAmmo < 0)
                {
                    bulletsInClip = totalAmmo + clipSize;
                    totalAmmo = 0;
                }
                //animatorComp.SetTrigger("Reload");
                //sounds[2].Play();
            }
            // need to wait two seconds before executing next line
            // can't fire while reloading
        }

        else
        {
            //Remove muzzleflash if not firing
            PhotonNetwork.Destroy(muzzleFlash);
            // if (shoot != null)
            //{
            //  shoot.Stop();
            //}
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
