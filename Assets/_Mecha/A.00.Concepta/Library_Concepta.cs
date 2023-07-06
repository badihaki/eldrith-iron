using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Library_Concepta : MonoBehaviour
{
    public GameObject specialProj;

    public void SpecialAttackAnim()
    {
        GameObject specProjClone = Instantiate(specialProj, transform.Find("WeaponPoint").position, Quaternion.identity);
        Destroy(specProjClone, 5.5f);
    }

}
