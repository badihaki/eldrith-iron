using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Pilot Filename", menuName = "PC/Create A New Pilot")]
public class Pilot_SO : ScriptableObject
{
    public string pilotName;
    public int handling; // how fast the pilot makes the mech moves around the screen
    public int guts; // how much meter the pilot builds for the mech's special move
    public Sprite portrait;

    public string introText;
}
