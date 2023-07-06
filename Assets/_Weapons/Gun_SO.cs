using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Gun",menuName ="Weapons/Gun")]
public class Gun_SO : Weapon_SO
{
    [Header("Assets")]
    public GameObject projectile;
    [Header("Stats")]
    public float projSpeed;

}
