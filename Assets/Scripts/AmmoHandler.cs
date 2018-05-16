using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmmoHandler : MonoBehaviour {

    [Header("Set in Inspector")]
    public Text magAmmo;
    public Text totalAmmo;
    public Image currentWeaponImage;
    public Image secondaryWeaponImage;

    public static void DisplayAmmo(int bulletsInClip, int totalAmmoVar)
    {
        //if (magAmmo != null)
        //    magAmmo.text = bulletsInClip.ToString();
        //if (totalAmmo != null)
        //    totalAmmo.text = totalAmmoVar.ToString();
    }
}
