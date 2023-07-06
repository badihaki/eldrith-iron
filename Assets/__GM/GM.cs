using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GM : MonoBehaviour
{
    [Header("Curent Pilot,Mech & Weapon")]
    public string header1 = "---";
    [field: SerializeField] public Pilot_SO pilot { get; private set; }
    public void SetPilot(Pilot_SO _pilot) => pilot = _pilot;
    [field: SerializeField] public Mecha_SO mech { get; private set; }
    public void SetMecha(Mecha_SO _mecha) => mech = _mecha;
    [field: SerializeField] public Weapon_SO weapon { get; private set; }
    public void SetWeapon(Weapon_SO _weapon) => weapon = _weapon;

    [field: SerializeField] public PilotCrew PilotCrew { get; private set; }
    [field: SerializeField] public MechHangar MechHangar { get; private set; }
    [field: SerializeField] public WeaponStash WeaponStash { get; private set; }

    // make a single instance
    private static GM instance;
    
    // mobile??
    [Header("Are we in mobile mode?")]
    public string header2 = "---";
    [field: SerializeField] public bool isMobileMode { get; private set; }

    // state machine
    [Header("State Machine")]
    public string currentStateName = "---";
    public GameStateMachine StateMachine { get; private set; }
    public MenuState MenuState { get; private set; }
    public GameplayState GameplayState { get;private set; }


    // Start is called before the first frame update
    void Start()
    {
        PilotCrew = GetComponent<PilotCrew>();
        if (PilotCrew.pilotList.Count > 0) SetPilot(PilotCrew.pilotList[0]);
        MechHangar = GetComponent<MechHangar>();
        if (MechHangar.mechList.Count > 0) SetMecha(MechHangar.mechList[0]);
        // I need to delete late
        mech.AddFuel(mech.maxFuel); // gives max fuel at the beginning, GODMODE
        // I need to delete late
        WeaponStash = GetComponent<WeaponStash>();
        if (WeaponStash.weaponList.Count > 0) SetWeapon(WeaponStash.weaponList[0]);

        // initialize the state machine
        StateMachine = new GameStateMachine();
        MenuState = new MenuState(this, StateMachine, "In-Menu");
        GameplayState = new GameplayState(this, StateMachine, "Gameplay");
        
        // start state machine
        StateMachine.InitializeGameStateMachine(MenuState);

        ThereCanBeOnlyOne();
    }

    public void InitializePlayerCharacter() // use this to start a level!!
    {
        var pc = Instantiate(mech.actor, GameObject.Find("PlayerStart").transform.position, Quaternion.identity);
        pc.GetComponent<PCControllerMecha>().pilot = pilot;
        pc.GetComponent<PCControllerMecha>().weapon = weapon;
    }



    #region scene manager

    public void StartLevelByName(string levelName)
    {
        SceneManager.LoadScene(levelName);
    }

    public void StartLevelByIndex(int indexNumber)
    {
        SceneManager.LoadScene(indexNumber);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ThereCanBeOnlyOne()
    {
        Screen.SetResolution(1920, 1080, true, 60);
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

        #endregion
    }
