using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Sword", menuName = "Weapons/Sword")]
public class Sword_SO : Weapon_SO
{
    [Header("Assets")]
    public GameObject slashEffect; // the weapon may use the same slash effect as others
    public AudioClip hitSoundFX; // the weapon may sound diff from others
}