using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Weapon_SO : ScriptableObject
{
    public Sprite image;
    public string weaponName;
    public int damage;
    public float atkRate;
    public float heatUsage;
    public float heatDispersalRate;
    public Vector3 weaponForce;
    /*
     * Weapon force notes
     * the force that'll push back on the player
     * if melee, it'll push forward, so +x
     * if ranged, it'll push back, so -x
     */
    public AudioClip weaponSound;
    public enum WeaponType
    {
        gun,
        sword
    }
    public WeaponType weaponType;

}
