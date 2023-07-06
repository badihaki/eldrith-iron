using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HangarMenuScr : MonoBehaviour
{
    [Header("Mech stuff")]
    [SerializeField] private List<Mecha_SO> mechList;
    public GameObject mechButton;
    private GameObject currentMechImage;

    [Header("Sword stuff")]
    [SerializeField] private List<Sword_SO> swordList;
    public GameObject swordBut;
    private GameObject currentSwordImage;
    [SerializeField] private Sword_SO actSword;

    [Header("Gun stuff")]
    [SerializeField] private List<Gun_SO> gunList;
    public GameObject gunBut;
    private GameObject currentGunImage;
    [SerializeField] private Gun_SO actGun;

    private GM gm;

    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.Find("Game Master").GetComponent<GM>();
        currentMechImage = GameObject.Find("CurrentPilot");
        mechList.Clear(); // make sure there are no pilots in the list
        gunList.Clear();
        swordList.Clear();
        PopulateMechList(); // then populate the list
        PopulateGunList();
        PopulateSwordList();
        CreateMechButtons();
        CreateGunButtons();
        CreateSwordButtons();

        Transform menuInfo = GameObject.Find("Mech Select Menu").transform.Find("Mech Info");
        menuInfo.transform.Find("Name").GetComponent<TMPro.TextMeshProUGUI>().text = gm.GetComponent<GM>().mech.mechName;
        menuInfo.transform.Find("Armor").GetComponent<TMPro.TextMeshProUGUI>().text = "Armor: " + gm.GetComponent<GM>().mech.armor.ToString();
        menuInfo.transform.Find("Fuel").GetComponent<TMPro.TextMeshProUGUI>().text = "Fuel: " + gm.GetComponent<GM>().mech.currentFuel.ToString() + " / " + gm.GetComponent<GM>().mech.maxFuel.ToString();
        menuInfo.transform.Find("Damage").GetComponent<TMPro.TextMeshProUGUI>().text = "Damage Output: " + gm.GetComponent<GM>().mech.damageOutput.ToString();
        menuInfo.transform.Find("Thrusters").GetComponent<TMPro.TextMeshProUGUI>().text = "Thrusters: " + gm.GetComponent<GM>().mech.thrusters.ToString();
    }

    private void PopulateMechList()
    {
        foreach (Mecha_SO mecha in gm.GetComponent<MechHangar>().mechList)
        {
            mechList.Add(mecha);
        }
    }

    private void CreateMechButtons()
    {
        foreach (Mecha_SO mecha in mechList)
        {
            var buttonObject = Instantiate(mechButton, GetComponent<HangarPanels>().mechPanel.transform.Find("Mech Select"));
            buttonObject.GetComponent<MechButtonScr>().InitButton(mecha);
        }
    }


    private void PopulateSwordList()
    {
        foreach (Sword_SO sword in gm.GetComponent<WeaponStash>().swordList)
        {
            swordList.Add(sword);
        }
    }
    private void CreateSwordButtons()
    {
        foreach (Sword_SO sword in swordList)
        {
            var buttonObject = Instantiate(swordBut, GetComponent<HangarPanels>().swordPanel.transform.Find("Select"));
            buttonObject.GetComponent<SwordButtonScr>().InitButton(sword);
        }
    }



    private void PopulateGunList()
    {
        foreach (Gun_SO gun in gm.GetComponent<WeaponStash>().gunList)
        {
            gunList.Add(gun);
        }
    }
    private void CreateGunButtons()
    {
        foreach (Gun_SO gun in gunList)
        {
            var buttonObject = Instantiate(gunBut, GetComponent<HangarPanels>().gunPanel.transform.Find("Select"));
            buttonObject.GetComponent<GunButtonScr>().InitButton(gun);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SelectNewMecha(Mecha_SO newMecha)
    {
        gm.SetMecha(newMecha);
        if (newMecha.weaponUseType == Mecha_SO.UsesWeapon.gun && gm.GetComponent<GM>().weapon.weaponType != Weapon_SO.WeaponType.gun)
        {
            gm.SetWeapon(gm.WeaponStash.gunList[0]);
        }
        else if (newMecha.weaponUseType == Mecha_SO.UsesWeapon.sword && gm.GetComponent<GM>().weapon.weaponType != Weapon_SO.WeaponType.sword)
        {
            gm.SetWeapon(gm.GetComponent<WeaponStash>().swordList[0]);
        }
        Transform menuInfo = GameObject.Find("Mech Select Menu").transform.Find("Mech Info");
        menuInfo.transform.Find("Name").GetComponent<TMPro.TextMeshProUGUI>().text = gm.GetComponent<GM>().mech.mechName;
        menuInfo.transform.Find("Armor").GetComponent<TMPro.TextMeshProUGUI>().text = "Armor: " + gm.GetComponent<GM>().mech.armor.ToString();
        menuInfo.transform.Find("Fuel").GetComponent<TMPro.TextMeshProUGUI>().text = "Fuel: " + gm.GetComponent<GM>().mech.currentFuel.ToString() + " / " + gm.GetComponent<GM>().mech.maxFuel.ToString();
        menuInfo.transform.Find("Damage").GetComponent<TMPro.TextMeshProUGUI>().text = "Damage Output: " + gm.GetComponent<GM>().mech.damageOutput.ToString();
        menuInfo.transform.Find("Thrusters").GetComponent<TMPro.TextMeshProUGUI>().text = "Thrusters: " + gm.GetComponent<GM>().mech.thrusters.ToString();



        // armor, fuel, damage, thrusters

    }
    public void SelectNewSword(Sword_SO newBlade)
    {
        actSword = newBlade;
        gm.SetWeapon(newBlade);
        
        Transform menuInfo = GameObject.Find("Sword Select Menu").transform.Find("Info");
        menuInfo.transform.Find("Name").GetComponent<TMPro.TextMeshProUGUI>().text = gm.GetComponent<GM>().weapon.weaponName;
        menuInfo.transform.Find("Name").transform.Find("Image").GetComponent<Image>().sprite = gm.GetComponent<GM>().weapon.image;
        menuInfo.transform.Find("Damage").GetComponent<TMPro.TextMeshProUGUI>().text = "Base Power: " + gm.GetComponent<GM>().weapon.damage;
        menuInfo.transform.Find("Heat-Use").GetComponent<TMPro.TextMeshProUGUI>().text = "Heat Usage: " + gm.GetComponent<GM>().weapon.heatUsage;
        menuInfo.transform.Find("Dispersal").GetComponent<TMPro.TextMeshProUGUI>().text = "Heat Dispersal: " + gm.GetComponent<GM>().weapon.heatDispersalRate;
        menuInfo.transform.Find("AtkRate").GetComponent<TMPro.TextMeshProUGUI>().text = "Rate of Attack: " + gm.GetComponent<GM>().weapon.atkRate.ToString();
        

    }

    public void SelectNewGun(Gun_SO newGun) 
    {
        actGun = newGun;
        gm.SetWeapon(newGun);


        Transform menuInfo = GameObject.Find("Gun Select Menu").transform.Find("Info");
        menuInfo.transform.Find("Name").GetComponent<TMPro.TextMeshProUGUI>().text = gm.GetComponent<GM>().weapon.weaponName;
        menuInfo.transform.Find("Name").transform.Find("Image").GetComponent<Image>().sprite = gm.GetComponent<GM>().weapon.image;
        menuInfo.transform.Find("Damage").GetComponent<TMPro.TextMeshProUGUI>().text = "Base Power: " + gm.GetComponent<GM>().weapon.damage;
        menuInfo.transform.Find("Heat-Use").GetComponent<TMPro.TextMeshProUGUI>().text = "Heat Usage: " + gm.GetComponent<GM>().weapon.heatUsage;
        menuInfo.transform.Find("Dispersal").GetComponent<TMPro.TextMeshProUGUI>().text = "Heat Dispersal: " + gm.GetComponent<GM>().weapon.heatDispersalRate;
        menuInfo.transform.Find("AtkRate").GetComponent<TMPro.TextMeshProUGUI>().text = "Rate of Attack: " + gm.GetComponent<GM>().weapon.atkRate.ToString();
    }

}
