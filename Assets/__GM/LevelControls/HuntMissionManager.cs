using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class HuntMissionManager : MonoBehaviour
{

    private GM gm;

    [Header("Hold the score, show the score goal")]
    [SerializeField] private int score;
    [SerializeField] private int scoreGoal;
    [Header("Set the mininum and maximum score")]
    public int scoreMin = 35;
    public int scoreMax = 100;

    // public HuntMissionManager(int desiredScore, GM gameMaster)
    // {
    //     scoreGoal = desiredScore;
    //     gm = gameMaster;
    //     gm.StateMachine.ChangeState(gm.GameplayState);
    // }

    public void Initialize(int desiredScore, GM gameMaster)
    {
        scoreGoal = desiredScore;
        gm = gameMaster;
        gm.StateMachine.ChangeState(gm.GameplayState);
    }

    // Start is called before the first frame update
    // void Start()
    // {
    //     scoreGoal = UnityEngine.Random.Range(25, 75);
    // 
    //     gm = GameObject.Find("Game Master").GetComponent<GM>();
    // }

    // Update is called once per frame
    void Update()
    {
        UpdateUI();

    }

    private void UpdateUI()
    {
        transform.Find("Score").Find("ScoreText").GetComponent<TMPro.TextMeshProUGUI>().text = score.ToString() + " / " + scoreGoal.ToString();
    }

    public void AddToScore()
    {
        score += 1;
        if (score >= scoreGoal)
        {
            // win state in mission manager
            GetComponent<MissionManager>().WinState();
        }
    }

}
