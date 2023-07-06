using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharSelectScreenScr : MonoBehaviour
{
    [SerializeField] private List<Pilot_SO> pilotList;
    [SerializeField] private Pilot_SO selectedPilot;

    public GameObject pilotButton;

    [SerializeField] private GameObject pilotSelectMenu;

    private GM GameMaster;
    private GameObject currentPilotGO;

    // Start is called before the first frame update
    void Start()
    {
        GameMaster = GameObject.Find("Game Master").GetComponent<GM>();
        currentPilotGO = GameObject.Find("CurrentPilot");
        pilotSelectMenu = GameObject.Find("Pilot Select");
        pilotList.Clear(); // make sure there are no pilots in the list
        PopulatePilotList(); // then populate the list
        CreatePilotButtons();

    }

    

    // Update is called once per frame
    void Update()
    {
        currentPilotGO.GetComponent<Image>().sprite = GameMaster.GetComponent<GM>().pilot.portrait;
    }

    private void PopulatePilotList()
    {
        foreach(Pilot_SO pilot in GameMaster.GetComponent<PilotCrew>().pilotList)
        {
            pilotList.Add(pilot);
        }
    }
    private void CreatePilotButtons()
    {
        foreach(Pilot_SO pilot in pilotList)
        {
            var buttonObject = Instantiate(pilotButton, pilotSelectMenu.transform);
            buttonObject.GetComponent<PilotButtonScr>().InitButton(pilot);
            print("created new pilot " + pilot.pilotName + " for selection");
        }
    }

    public void SelectNewPilot(Pilot_SO replacePilot)
    {
        selectedPilot = replacePilot;
    }

    public void ConfirmNewPilot() => GameMaster.SetPilot(selectedPilot);

    public void GoHome()
    {
        GameObject.Find("Game Master").GetComponent<GM>().StartLevelByIndex(0);
    }
}
