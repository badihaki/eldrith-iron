using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddPilot : MonoBehaviour, IPilotContainer
{

    [SerializeField] private List<Pilot_SO> pilotList;
    [SerializeField] private Pilot_SO selectedPilot;

    public bool AddNewPilot(Pilot_SO pilot)
    {
        for (int i = 0; i < pilotList.Count; i++)
        {
            if (pilotList[i] == null)
            {
                pilotList[i] = pilot;
                return true;
            }
        }
        return false;
    }

    public bool ContainsPilot(Pilot_SO pilot)
    {
        for (int i = 0; i < pilotList.Count; i++)
        {
            if (pilotList[i] == pilot)
            {
                return true;
            }
        }
        return false;
    }

    public bool IsFull()
    {
        for (int i = 0; i < pilotList.Count; i++)
        {
            if (pilotList[i] == null)
            {
                return false;
            }
        }
        return true;
    }

    public int PilotCount(Pilot_SO pilot)
    {
        int number = 0;
        for (int i = 0; i < pilotList.Count; i++)
        {
            if (pilotList[i] == null)
            {
                number++;
            }
        }
        return number;
    }

    public bool RemovePilot(Pilot_SO pilot)
    {
        for (int i = 0; i < pilotList.Count; i++)
        {
            if (pilotList[i] == pilot)
            {
                pilotList[i] = null;
                return true;
            }
        }
        return false;
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
