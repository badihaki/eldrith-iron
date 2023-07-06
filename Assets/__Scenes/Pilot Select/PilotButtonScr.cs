using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PilotButtonScr : MonoBehaviour
{
    public Pilot_SO pilot;

    [SerializeField] private GameObject infoScreen;
    [SerializeField] private GameObject charSelectMenu;

    // Start is called before the first frame update
    void Start()
    {
        infoScreen = GameObject.Find("Pilot Info");
        charSelectMenu = GameObject.Find("CharacterSelectManager");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InitButton(Pilot_SO myPilot)
    {
        pilot = myPilot;
        GetComponent<Image>().sprite = pilot.portrait;
        transform.Find("Name").GetComponent<TMPro.TextMeshProUGUI>().text = pilot.name;
    }

    public void SelectPilot()
    {
        charSelectMenu.GetComponent<CharSelectScreenScr>().SelectNewPilot(pilot);
        infoScreen.transform.Find("Name").GetComponent<TMPro.TextMeshProUGUI>().text = pilot.pilotName;
        infoScreen.transform.Find("Name").transform.Find("Image").GetComponent<Image>().sprite = pilot.portrait;
        infoScreen.transform.Find("Handling").GetComponent<TMPro.TextMeshProUGUI>().text = "Mech Handling " + pilot.handling.ToString();
        infoScreen.transform.Find("Guts").GetComponent<TMPro.TextMeshProUGUI>().text = "Pilot Guts " + pilot.guts.ToString();
        infoScreen.transform.Find("IntroTxt").GetComponent<TMPro.TextMeshProUGUI>().text = pilot.introText;
    }
}
