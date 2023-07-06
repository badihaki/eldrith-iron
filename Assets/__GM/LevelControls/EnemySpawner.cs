using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Required")]
    [SerializeField]
    private GameObject[] enemyFormations;
    [Header("Spawn Timer Variables")]
    [SerializeField] private float timer;
    [SerializeField] private float maxAddTime;

    // Start is called before the first frame update
    void Start()
    {
        timer = maxAddTime; // set the timer
    }

    // Update is called once per frame
    void Update()
    {
        SpawnCountdown(); // make sure the countdown is happening every frame
    }

    private void SpawnCountdown()
    {
        if (timer > 0)
        {
            /*
             * If timer is more than zip, lower it by the delta time (in-game time)
             */
            timer -= Time.deltaTime;
        }
        else if (timer <= 0)
        {
            /*
             * If timer is 0 or less, 
             * create a new integer (formationIndex), and set it to a random number between 0
             * and the length of enemyFormations
             * Instantiate the enemyFormation at formationIndex
             * Create a floating number for the amount of time added to the timer
             * ^^^ Right now it's between 1, and the listed formTimeCost in the EnemyFormation script
             * That's a public float from another script, but I think I need a minimum time held in the same script, too
             * Finally, we add that time to the timer
             */
            int formationIndex = UnityEngine.Random.Range(0, enemyFormations.Length);
            Instantiate(enemyFormations[formationIndex], transform.position, Quaternion.identity);
            float addTime = UnityEngine.Random.Range(maxAddTime, maxAddTime + enemyFormations[formationIndex].GetComponent<EnemyFormation>().formTimeCost);
            Debug.Log("LOG:: Enemy Formation is " + enemyFormations[formationIndex].name);
            Debug.Log("LOG:: The time added to the enemy spawner: " + addTime);
            timer += addTime;
        }
    }
}
