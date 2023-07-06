using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class MissionManager : MonoBehaviour
{
    [Header("Mission Init")]
    [SerializeField] private MissionSO currentMission;
    [SerializeField] private MissionSO[] nextMissions;
    [SerializeField] private GM GM;

    [Header("UI Stuff")]
    [SerializeField] private GameObject mobileControls;
    [SerializeField] private GameObject winScreen;
    [SerializeField] private GameObject loseScreen;
    [SerializeField] private GameObject eliminationScore;

    [Header("Elimination")]
    [SerializeField] private int enemiesKilled;
    [SerializeField] private int expGained;

    // Start is called before the first frame update
    void Start()
    {
        mobileControls = GameObject.Find("Mobile Controls");
        GM = GameObject.Find("Game Master").GetComponent<GM>();
        GM.InitializePlayerCharacter();
        SetUI();
        InitializeMission();
    }

    private void SetUI()
    {
        if (GM.isMobileMode) mobileControls.SetActive(true);
        else mobileControls.SetActive(false);
        loseScreen.SetActive(false);
        winScreen.SetActive(false);
        eliminationScore.SetActive(false);
    }

    private void InitializeMission()
    {
        if (currentMission.initialMission == MissionSO.FirstMissionType.elimination)
        {
            eliminationScore.SetActive(true);
            HuntMissionManager secondaryManager = gameObject.AddComponent(typeof(HuntMissionManager)) as HuntMissionManager;
            secondaryManager.Initialize(UnityEngine.Random.Range(currentMission.minimumScore, currentMission.maximumScore), GM);
        }
        else if (currentMission.initialMission == MissionSO.FirstMissionType.survival)
        {
            // set parameters
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EnemyKilled()
    {
        enemiesKilled += 1;
        if (GetComponent<HuntMissionManager>() != null)
        {
            GetComponent<HuntMissionManager>().AddToScore();
        }
    }

    public void WinState()
    {
        // disable everything
        GameObject.Find("EnemySpawn").SetActive(false);
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject gameObject in enemies)
        {
            Destroy(gameObject);
        }
        GameObject playerChar = GameObject.FindGameObjectWithTag("Player");
        playerChar.SetActive(false);
        // completion checks need to be checked
        currentMission.MissionCompleted(GM);
        foreach (MissionSO mission in nextMissions)
        {
            mission.UnlockMission();
            print(mission.name + " Mission unlocked");
        }

        // lets open that win screen
        winScreen.SetActive(true);

    }
    public void FailureState()
    {
        GameObject.Find("EnemySpawn").SetActive(false);
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject gameObject in enemies)
        {
            Destroy(gameObject);
        }
        GameObject playerChar = GameObject.FindGameObjectWithTag("Player");
        playerChar.SetActive(false);


        loseScreen.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene(0);
    }
    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
