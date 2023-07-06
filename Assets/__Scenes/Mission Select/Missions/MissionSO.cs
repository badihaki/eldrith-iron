using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Mission",menuName ="AreaX-00")]
public class MissionSO : ScriptableObject
{
    [Header("Mission Locks")]
    public bool missionUnlocked; // if the mission is unlocked, it is available
    public bool missionComplete; // if the mission is completed
    public bool unlockablePilot; // if there is an unlockable pilot
    public Pilot_SO pilotUnlock;
    public bool unlockableMecha; // if there is an unlockable mecha
    public Mecha_SO mechaUnlock;
    public bool unlockableWeapon;
    public Weapon_SO weaponUnlock;

    public enum FirstMissionType
    {
        elimination,
        survival,
    }
    
    [Header("Mission Types")]
    public FirstMissionType initialMission;
    public bool missionOneHasBoss;
    public GameObject[] minibossPool;
    // ^^^ All of this is for Mission 1
    // If Mission 1 has a boss, Mission 2 should be a boss fight
    // If Mission 1 has another mission, specify it
    public enum SecondMisionType
    {
        nada,
        elimination,
        survival,
        boss,
    }
    public SecondMisionType secondMision;
    public bool missionTwoHasBoss;
    public GameObject[] bossPool;
    // ^^^ All of this for Mission 2
    // If Mission 2 has a boss, fuckit, throw a boss in there

    [Header("For Elimination Missions")]
    public int minimumScore;
    public int maximumScore;

    [Header("For Survival Missions")]
    public float minimumSurviveTime;
    public float maximumSurviveTime;

    [Header("UI Stuff")]
    public string missionTitle;
    public Sprite openingImage;
    public string missionInfo;

    public void UnlockMission()
    {
        missionUnlocked = true;
        Debug.Log("new mission unlocked, " + missionTitle);
    }

    public void MissionCompleted(GM gm)
    {
        missionComplete = true;
        if (unlockablePilot == true)
        {
            PilotCrew pilotCrewList = gm.GetComponent<PilotCrew>();
            pilotCrewList.AddPilot(pilotUnlock);
        }
        if (unlockableMecha == true)
        {
            MechHangar hangar = gm.GetComponent<MechHangar>();
            hangar.AddNewMechaToHangar(mechaUnlock);
        }
        if (unlockableWeapon == true)
        {
            WeaponStash weaponStash = gm.GetComponent<WeaponStash>();
            weaponStash.AddNewWeapon(weaponUnlock);
        }
    }
}