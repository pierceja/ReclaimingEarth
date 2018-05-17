using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ModelSwitch : MonoBehaviour
{
    bool pistol;
    bool vert;

    // Use this for initialization
    void Start()
    {

        transform.Find("TabooPistol").gameObject.SetActive(false);
        transform.Find("Vert").gameObject.SetActive(true);
        pistol = false;
        vert = true;

    }



    // Update is called once per frame

    void Update()
    {

        if (Input.GetKeyDown("e"))

        {

            if (pistol)

            {

                transform.Find("TabooPistol").gameObject.SetActive(false);

                transform.Find("Vert").gameObject.SetActive(true);

                pistol = false;

                vert = true;

                
            }

            else

            {

                transform.Find("TabooPistol").gameObject.SetActive(true);

                transform.Find("Vert").gameObject.SetActive(false);

                pistol = true;

                vert = false;

                
            }
            DisplayWeaponImages();
        }



    }

    void DisplayWeaponImages()
    {
        Image currentWeaponImage = GameObject.Find("/Player HUD/WeaponUI/CurrentWeaponImage").GetComponent<Image>();
        Image secondaryWeaponImage = GameObject.Find("/Player HUD/WeaponUI/SecondaryWeaponImage").GetComponent<Image>();
        Sprite vertPic = Resources.Load<Sprite>("VertPic");
        Sprite pistolPic = Resources.Load<Sprite>("TabooPistolPic");

        if (pistol)
        {
            currentWeaponImage.sprite = pistolPic;
            secondaryWeaponImage.sprite = vertPic;
        } else
        {
            currentWeaponImage.sprite = vertPic;
            secondaryWeaponImage.sprite = pistolPic;
        }
        
    }
}