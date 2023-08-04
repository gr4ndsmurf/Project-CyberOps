using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InGameUIManager : MonoBehaviour
{
    [SerializeField] private Weapon pistolWP;
    [SerializeField] private Weapon rifleWp;

    [SerializeField] private WeaponSwitching wpS;

    [SerializeField] private TextMeshProUGUI ammoText;
    [SerializeField] private TextMeshProUGUI magazineText;

    [SerializeField] private GameObject ammoUI;
    [SerializeField] private GameObject reloadUI;

    [SerializeField] private GameObject PistolOutofAmmoUI;
    [SerializeField] private GameObject RifleOutofAmmoUI;

    // Update is called once per frame
    void Update()
    {
        if (wpS.selectedWeapon == 0)
        {
            ammoUI.SetActive(false);
        }
        else if (wpS.selectedWeapon == 1)
        {
            ammoUI.SetActive(true);
            ammoText.text = pistolWP.currentAmmo.ToString();
            magazineText.text = pistolWP.currentAmmoBox.ToString();
        }
        else if (wpS.selectedWeapon == 2)
        {
            ammoUI.SetActive(true);
            ammoText.text = rifleWp.currentAmmo.ToString();
            magazineText.text = rifleWp.currentAmmoBox.ToString();
        }

        if (pistolWP.isReloading || rifleWp.isReloading)
        {
            reloadUI.SetActive(true);
        }
        else
        {
            reloadUI.SetActive(false);
        }


        if (wpS.selectedWeapon == 1)
        {
            if (pistolWP.currentAmmoBox == 0 && pistolWP.currentAmmo == 0)
            {
                if (!reloadUI.activeInHierarchy)
                {
                    PistolOutofAmmoUI.SetActive(true);
                }
            }
        }
        else
        {
            PistolOutofAmmoUI.SetActive(false);
        }


        if (wpS.selectedWeapon == 2)
        {
            if (rifleWp.currentAmmoBox == 0 && rifleWp.currentAmmo == 0)
            {
                if (!reloadUI.activeInHierarchy)
                {
                    RifleOutofAmmoUI.SetActive(true);
                }
            }
        }
        else
        {
            RifleOutofAmmoUI.SetActive(false);
        }
    }
}