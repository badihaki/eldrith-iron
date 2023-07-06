using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(EnemyAttack))]
[RequireComponent(typeof(EnemyLoot))]
[RequireComponent(typeof(EnemyExplode))]

public class EnemyActor : MonoBehaviour
{
    // this will be the root info
    [SerializeField] private Enemy_SO enemy;
    [Header("Basic stats for survival")]
    [SerializeField]
    private int currentHP;
    Rigidbody2D controller;

    [Header("UI variables")]
    [SerializeField] private Slider healthbar;
    [SerializeField] private Vector3 healthBarOffset;

    [Header("Managers")]
    [SerializeField]private HuntMissionManager huntMissionManager;

    // Start is called before the first frame update
    void Start()
    {
        InitEnemy();
        
    }

    private void InitEnemy()
    {
        currentHP = enemy.health;
        controller = GetComponent<Rigidbody2D>();
        controller.gravityScale = 0.0f;
        GetComponent<BoxCollider2D>().isTrigger = true;

        // set up healthbar
        healthbar = transform.Find("UI").Find("Healthbar").GetComponent<Slider>();
        healthbar.maxValue = enemy.health;

        // set up attack
        GetComponent<EnemyAttack>().InitializeEnemyAttack(enemy);

        // set movement based on enemy class
        if (enemy.enemyType == Enemy_SO.EnemyClassType.kamikaze)
        {
            KamikazeMove movement = gameObject.AddComponent(typeof(KamikazeMove)) as KamikazeMove;
            movement.InitializeMovement(enemy);
        }
        else if (enemy.enemyType == Enemy_SO.EnemyClassType.fighter)
        {
            EnemyFighterMovement movement = gameObject.AddComponent(typeof(EnemyFighterMovement)) as EnemyFighterMovement;
            movement.InitializeMovement(enemy);
        }
    }

    // Update is called once per frame
    void Update()
    {
        UpdateEnemyUI();
        if (currentHP <= 0)
        {
            KillEnemy();
        }
    }
    

    private void UpdateEnemyUI()
    {
        if (currentHP == enemy.health)
        {
            if (healthbar.gameObject.activeSelf == true)
            {
                healthbar.gameObject.SetActive(false);
            }
            
        }
        else if (currentHP < enemy.health)
        {
            if (healthbar.gameObject.activeSelf == false)
            {
                healthbar.gameObject.SetActive(true);
            }
            healthbar.transform.position = Camera.main.WorldToScreenPoint(transform.position + healthBarOffset);
            healthbar.value = currentHP;
        }
    }

    private void KillEnemy()
    {
        /*
         * Ideally we will play a death animation
         * experience will be given to the pilot
         * spawn debris/viscera
         * finally, we destroy the enemy
         */
        if (GameObject.Find("MissionManager").GetComponent<MissionManager>() != null)
        {
            GameObject.Find("MissionManager").GetComponent<MissionManager>().EnemyKilled();
        }
        
        // drop an item
        GetComponent<EnemyLoot>().DropItem();

        // if there is a way to explode, fuckin explode
        if (GetComponent<EnemyExplode>() != null)
        {
            // explode the enemy
            GetComponent<EnemyExplode>().ExplodeEnemy();
        }

        // lets get rid of this enemy
        Destroy(this.gameObject);
    }

    public void TakeDamage(int damageGiven, bool isPlayerGettinGuts)
    {
        if (isPlayerGettinGuts == true)
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<PCControllerMecha>().AddGuts();
            currentHP -= damageGiven;
        }
        else
        {
            currentHP -= damageGiven;
        }
        
    }
}
