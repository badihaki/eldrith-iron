using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PilotCrew : MonoBehaviour
{
    public List<Pilot_SO> pilotList;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddPilot(Pilot_SO newCrew)
    {
        if (pilotList.IndexOf(newCrew) < 0) // if the object isn't found, this should return -1
        {
            pilotList.Add(newCrew);
        }
    }
}
