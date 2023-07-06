using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponStash : MonoBehaviour
{
    public List<Weapon_SO> weaponList;
    public List<Sword_SO> swordList;
    public List<Gun_SO> gunList;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddNewWeapon(Weapon_SO newWeapon)
    {
        if (weaponList.IndexOf(newWeapon) < 0) // if the object isn't found, this should return -1
        {
            weaponList.Add(newWeapon);
            if (newWeapon.weaponType == Weapon_SO.WeaponType.gun)
            {
                if (gunList.IndexOf(newWeapon as Gun_SO) < 0)
                {
                    gunList.Add(newWeapon as Gun_SO);
                    print("we added a new weapon: " + newWeapon.weaponName);
                }
            }
            else if (newWeapon.weaponType == Weapon_SO.WeaponType.sword)
            {
                if(swordList.IndexOf(newWeapon as Sword_SO) < 0)
                {
                    swordList.Add(newWeapon as Sword_SO);
                    print("we added a new weapon: " + newWeapon.weaponName);
                }
            }
        }
    }
}
