using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using Cinemachine;

[RequireComponent(typeof(PlayerInput))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CapsuleCollider2D))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(PCMechaMove))]
[RequireComponent(typeof(PCMechaAttack))]
[RequireComponent(typeof(PCCamShakeInitializer))]

public class PCControllerMecha : MonoBehaviour
{
    private PlayerControls inputActions;
    [Header("Curent Pilot and the Mech they drive")]    
    public Pilot_SO pilot;
    public Mecha_SO mech;
    public Weapon_SO weapon;
    
    [Header("Stats from pilot and mech info")]
    [SerializeField] private int currentHP;
    [SerializeField] private float currentFuel;
    [SerializeField] private float currentHeat;
    public bool overHeated;
    [SerializeField] private int guts;
    GameObject specialBut;

    [Header("Misc")]
    [SerializeField] private GameObject playerUI;
    [SerializeField] private GameObject mobileConts;
    [SerializeField] private Animator anim;
    [SerializeField] private CinemachineImpulseSource camShaker;

    [Header("Is the player in a cutscene?")]
    public bool inCutscene;

    private void Awake()
    {
        inputActions = new PlayerControls();
        camShaker = GetComponent<CinemachineImpulseSource>();
    }
    private void OnEnable()
    {
        inputActions.Enable();
    }
    private void OnDisable()
    {
        inputActions.Disable();
    }

    // Start is called before the first frame update
    void Start()
    {
        // initialize player stats
        currentFuel = mech.currentFuel;
        currentHP = mech.armor;
        currentHeat = 0;
        overHeated = false;
        guts = 0;
        mobileConts = GameObject.Find("Mobile Controls");
        if (mobileConts != null)
        {
            specialBut = mobileConts.transform.Find("Special").gameObject;
        }
        playerUI = GameObject.Find("PCCanvas");
        playerUI.transform.Find("Heat").Find("Slider").GetComponent<Slider>().maxValue = 100;
        playerUI.transform.Find("Pilot").Find("Guts").GetComponent<Slider>().maxValue = 100;
        anim = GetComponent<Animator>();


        // initialize weapon stuffs
        if (weapon.weaponType == Weapon_SO.WeaponType.gun)
        {
            GetComponent<PCMechaAttack>().atkType = PCMechaAttack.AtkType.range;
            GetComponent<PCMechaAttack>()._gun = (Gun_SO)weapon;
        }
        else if (weapon.weaponType == Weapon_SO.WeaponType.sword)
        {
            GetComponent<PCMechaAttack>().atkType = PCMechaAttack.AtkType.melee;
            GetComponent<PCMechaAttack>()._sword = (Sword_SO)weapon;
        }
    }

    // Update is called once per frame
    void Update()
    {
        UseFuel();
        ManageHeat();
        PCUI();
    }

    private void PCUI()
    {
        // pilot portrait
        playerUI.transform.Find("Pilot").GetComponent<Image>().sprite = pilot.portrait;

        // health
        playerUI.transform.Find("Health").Find("Current").GetComponent<TMPro.TextMeshProUGUI>().text = currentHP.ToString();
        playerUI.transform.Find("Health").Find("Max").GetComponent<TMPro.TextMeshProUGUI>().text = mech.armor.ToString();

        // fuel
        playerUI.transform.Find("Fuel").Find("Slider").GetComponent<Slider>().maxValue = mech.maxFuel;
        playerUI.transform.Find("Fuel").Find("Slider").GetComponent<Slider>().value = currentFuel;

        // weapon and heat
        transform.Find("WeaponPoint").GetComponent<SpriteRenderer>().sprite = weapon.image;
        playerUI.transform.Find("Heat").Find("Slider").GetComponent<Slider>().value = currentHeat;
        if (overHeated == false)
        {
            playerUI.transform.Find("Heat").Find("Overheat Warning").gameObject.SetActive(false);
        }
        else if (overHeated == true)
        {
            playerUI.transform.Find("Heat").Find("Overheat Warning").gameObject.SetActive(true);

        }


        //special button
        playerUI.transform.Find("Pilot").Find("Guts").GetComponent<Slider>().value = guts;
        if (guts < 100)
        {
            if(mobileConts != null)
            {
                if (specialBut.activeSelf == true)
                {
                    specialBut.SetActive(false);
                }
            }
            
        }
        else if (guts >= 100)
        {
            if (mobileConts != null)
            {
                if (specialBut.activeSelf == false)
                {
                    specialBut.SetActive(true);
                }
            }
        }
    }

    #region gutsy shit for gutsy people
    private void ManageGuts()
    {
        if (guts > 100)
        {
            guts = 100;
        }
        else if (guts < 0)
        {
            guts = 0;
        }
    }
    public void AddGuts()
    {
        guts += pilot.guts;
    }
    public void UseGutsforGuts()
    {
        if (guts >= 100)
        {
            guts -= guts;
        }
        else
        {
            print("Oooh, shit, there is not enough guts how did this happen!?");
            Debug.LogError("Current guts is " + guts + "and thats a mufuckin problem!");
        }
    }

#endregion

    #region heat management
    private void ManageHeat()
    {
        if (currentHeat > 0 && currentHeat < 100)
        {
            if (overHeated == false)
            {
                currentHeat -= Time.deltaTime * weapon.heatDispersalRate;
            }
            else if (overHeated == true)
            {
                currentHeat -= Time.deltaTime * (weapon.heatDispersalRate * 2.75f);
            }
        }
        else if (currentHeat >= 100)
        {
            currentHeat = 100;
            currentHeat -= Time.deltaTime * weapon.heatDispersalRate;
            overHeated = true;
        }
        else if (currentHeat <= 0)
        {
            currentHeat = 0;
            if (overHeated == true) overHeated = false;
        }
        if (currentHeat <= 50)
        {
            if (overHeated == true) overHeated = false;
        }
    }

    public void UseHeat()
    {
        currentHeat += weapon.heatUsage;
    }
    #endregion

    #region manage fuel
    private void UseFuel()
    {
        currentFuel -= Time.deltaTime;
        if (currentFuel > mech.maxFuel)
        {
            currentFuel = mech.maxFuel;
        }
        else if (currentFuel <= 0)
        {
            Failure();
        }
    }
    public void GetFuel(float refuel)
    {
        currentFuel += refuel;
    }
    #endregion

    #region health and damage
    public void TakeDamage(int damage)
    {
        currentHP -= damage;
        anim.SetTrigger("hurt");
        camShaker.GenerateImpulse();
        GameObject.Find("Game Master").GetComponent<GMTimeManager>().HitStop(0.1f);
        if (currentHP <= 0) Failure();
    }
    public void GetArmor(int armorUp)
    {
        currentHP += armorUp;
        if (currentHP >= mech.armor)
        {
            currentHP = mech.armor;
        }
    }
    public void AnimHurt()
    {
        inCutscene = true;
        GetComponent<PCMechaMove>().GetMoveInput(Vector2.zero);
    }
    public void AnimHurtEnd()
    {
        inCutscene = false;
        Vector2 moveInput = inputActions.Mecha.Move.ReadValue<Vector2>();
        GetComponent<PCMechaMove>().GetMoveInput(moveInput);
    }
    #endregion

    #region failure states
    private void Failure()
    {
        currentHP = 0;
        currentHeat = 0;
        currentFuel = 0;
        if (GameObject.Find("MissionManager").GetComponent<MissionManager>() != null)
        {
            GameObject.Find("MissionManager").GetComponent<MissionManager>().FailureState();
        }
        Destroy(this.gameObject);
    }
    #endregion

    #region Input Actions

    public void Special(InputAction.CallbackContext context)
    {
        if (inCutscene == false)
        {
            if (context.performed && guts >= 100)
            {
                GetComponent<PCMechaAttack>().Special();
            }
        }
        
    }

    public void Attack(InputAction.CallbackContext context)
    {
        if (inCutscene == false)
        {
            if (overHeated == false)
            {
                if (context.performed)
                {
                    GetComponent<PCMechaAttack>().attacking = true;
                }
                else if (context.canceled)
                {
                    GetComponent<PCMechaAttack>().attacking = false;

                }
            }
            else if (overHeated == true)
            {
                GetComponent<PCMechaAttack>().attacking = false;
            }
        }
        else
        {
            GetComponent<PCMechaAttack>().attacking = false;
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if (inCutscene == false)
        {
            Vector2 moveInput = context.ReadValue<Vector2>();
            GetComponent<PCMechaMove>().GetMoveInput(moveInput);
        }
    }

    #endregion
}
