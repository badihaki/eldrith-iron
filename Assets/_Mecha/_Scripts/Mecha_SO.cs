using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Mech Filename",menuName ="PC/Create A New Mech")]
public class Mecha_SO : ScriptableObject
{
    public string mechName;
    public enum UsesWeapon
    {
        gun,sword,
    }
    public UsesWeapon weaponUseType;
    [Header("Mech Stats")]
    public int armor; // health of the mech
    public float maxFuel;
    public float currentFuel;
    public int damageOutput; // how much damage the mech does
    public int thrusters; // how fast the mech moves through the level, serves kinda as difficulty
    [Header("In-game Actor Components")]
    public GameObject actor;
    public Sprite portrait;

    public void UseFuel(float fuel)
    {
        currentFuel -= fuel;
    }
    public void AddFuel(float fuel)
    {
        currentFuel += fuel;
        if (currentFuel > maxFuel) currentFuel = maxFuel;
    }
}
