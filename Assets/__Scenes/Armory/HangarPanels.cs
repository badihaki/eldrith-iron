using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HangarPanels : MonoBehaviour
{
    [Header("Panels")]
    public GameObject mechPanel;
    public GameObject gunPanel;
    public GameObject swordPanel;

    [Header("Buttons")]
    public GameObject swordInventoryBut;
    public GameObject gunInventoryBut;

    private GameObject gm;


    // Start is called before the first frame update
    void Start()
    {
        mechPanel = GameObject.Find("Mech Select Menu");
        gunPanel = GameObject.Find("Gun Select Menu");
        swordPanel = GameObject.Find("Sword Select Menu");
        gm = GameObject.Find("Game Master");
        ActivateMechPanel();
    }

    // Update is called once per frame
    void Update()
    {
        if (mechPanel.activeSelf == true)
        {
            if (gm.GetComponent<GM>().mech.weaponUseType == Mecha_SO.UsesWeapon.gun)
            {
                if (swordInventoryBut.activeSelf == true)
                {
                    swordInventoryBut.SetActive(false);
                }
                if (gunInventoryBut.activeSelf != true)
                {
                    gunInventoryBut.SetActive(true);
                }


            }
            else if(gm.GetComponent<GM>().mech.weaponUseType == Mecha_SO.UsesWeapon.sword)
            {
                if (gunInventoryBut.activeSelf == true)
                {
                    gunInventoryBut.SetActive(false);
                }
                if (swordInventoryBut.activeSelf != true)
                {
                    swordInventoryBut.SetActive(true);
                }

            }
        }
    }

    public void ActivateMechPanel()
    {
        CheckWeaponButtons(); // just to make sure the correct weapon button is playing

        mechPanel.SetActive(true);
        gunPanel.SetActive(false);
        swordPanel.SetActive(false);

        
    }

    private void CheckWeaponButtons()
    {
        if (gm.GetComponent<GM>().mech.weaponUseType == Mecha_SO.UsesWeapon.gun)
        {
            if (swordInventoryBut.activeSelf == true)
            {
                swordInventoryBut.SetActive(false);
                gunInventoryBut.SetActive(true);
            }

        }
        else if (gm.GetComponent<GM>().mech.weaponUseType == Mecha_SO.UsesWeapon.sword)
        {
            if (gunInventoryBut.activeSelf == true)
            {
                swordInventoryBut.SetActive(true);
                gunInventoryBut.SetActive(false);
            }
        }
    }

    public void ActivateGunPanel()
    {
        gunPanel.SetActive(true);
        mechPanel.SetActive(false);
        swordPanel.SetActive(false);
    }

    public void ActivateSwordPanel()
    {
        swordPanel.SetActive(true);
        gunPanel.SetActive(false);
        mechPanel.SetActive(false);
    }

    public void GoHome()
    {
        GameObject.Find("Game Master").GetComponent<GM>().StartLevelByIndex(0);
    }

    // end of the line
}
