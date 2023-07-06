using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(CircleCollider2D))]
public class WeaponPickupScript : MonoBehaviour
{
    [Header("Setup")]
    [SerializeField] private bool active;
    [SerializeField] private float inactiveTime;
    [SerializeField] private bool dutyFinished;
    private Rigidbody2D rb;
    private Animator anim;
    private CircleCollider2D sensoryField;


    [Header("Stats")]
    [SerializeField] private int health;
    [SerializeField] private int currentHealth;
    [SerializeField] private int speed;
    [SerializeField] private int damage;
    [SerializeField] private float lifetime;

    [Header("Attacking")]
    public GameObject projectile;
    [SerializeField] private float atkRate, atkTimer;
    [SerializeField] int currentShootCount;
    [SerializeField] int totalShootCount;

    [Header("Following the Player")]
    [SerializeField] private Transform playerLoc;
    [SerializeField] private Vector2 startPos, targetPos, bobAmount;
    [SerializeField] private float bobDistance, followDistance;
    [SerializeField] private float currentDistanceFromPlayer;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = health;
        currentShootCount = totalShootCount;
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;
        sensoryField = GetComponent<CircleCollider2D>();
        sensoryField.isTrigger = true;
        active = false;
        atkTimer = atkRate;
    }

    // Update is called once per frame
    void Update()
    {
        if (active == true)
        {
            DockToPlayer(); // works as intended
            AutoAttack(); // attack using the attackrate
            LifeTimer(); // control how long I am alive???
        }
        else
        {
            rb.velocity = new Vector2(-speed * 0.75f, rb.velocity.y);
            inactiveTime -= Time.deltaTime;
            if (inactiveTime <= 0)
            {
                Retreat();
            }
        }
    }

    private void LifeTimer()
    {
        if (lifetime > 0)
        {
            lifetime -= Time.deltaTime;
        }
        else if (lifetime <= 0)
        {
            Retreat();
        }
    }

    private void AutoAttack()
    {
        if (atkTimer <= 0)
        {
            if (totalShootCount == 0) // if there's no total shoot count, we shoot once
            {
                GameObject projSpawn = Instantiate(projectile, transform.position, Quaternion.identity);
                projSpawn.GetComponent<PlayerProjectile>().damage = damage;
                projSpawn.GetComponent<PlayerProjectile>().projSpeed = speed * 1.55f;
                atkTimer = atkRate;
            }
            else
            {
                if (currentShootCount > 0)
                {
                    GameObject projSpawn = Instantiate(projectile, transform.position, Quaternion.identity);
                    projSpawn.GetComponent<PlayerProjectile>().damage = damage;
                    projSpawn.GetComponent<PlayerProjectile>().projSpeed = speed * 1.55f;
                    currentShootCount--;
                    atkTimer = atkRate * 0.15f;
                }
                else if (currentShootCount <= 0)
                {
                    GameObject projSpawn = Instantiate(projectile, transform.position, Quaternion.identity);
                    projSpawn.GetComponent<PlayerProjectile>().damage = damage;
                    projSpawn.GetComponent<PlayerProjectile>().projSpeed = speed * 1.55f;
                    atkTimer = atkRate;
                    currentShootCount = totalShootCount;
                }
            }
            
        }
        else if (atkTimer > 0)
        {
            atkTimer -= Time.deltaTime;
        }
    }

    private void DockToPlayer()
    {
        startPos = transform.position;
        currentDistanceFromPlayer = Vector2.Distance(startPos, playerLoc.position);
        if (currentDistanceFromPlayer >= followDistance)
        {
            targetPos = playerLoc.position;
            this.transform.position = Vector2.MoveTowards(startPos, targetPos, speed * Time.deltaTime);
        }
        if (currentDistanceFromPlayer <= bobDistance)
        {
            targetPos = new Vector2(startPos.x + (bobAmount.x * Mathf.Sin(Time.time * speed * 1.25f)),
                                    startPos.y + (bobAmount.y * Mathf.Sin(Time.time * speed * 2.25f)));
            this.transform.position = Vector2.MoveTowards(startPos, targetPos, speed * Time.deltaTime);
        }
        // transform.position = playerLoc.position;
    }

    public void TakeDamage(int dmg)
    {
        currentHealth -= dmg;
    }

    private void Dead()
    {
        rb.gravityScale = 1.2f;
        Destroy(gameObject, 3.5f);
    }
    private void Retreat()
    {
        active = false;
        dutyFinished = true;
        rb.AddForce(new Vector2(UnityEngine.Random.Range(3, 10), UnityEngine.Random.Range(5, 8)), ForceMode2D.Impulse);
        rb.gravityScale = 1;
        Destroy(gameObject, 5f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (active == false && dutyFinished == false)
        {
            if (collision.tag == "Player")
            {
                playerLoc = collision.transform;
                transform.tag = "Buddy";
                active = true;
            }
        }
        else if (active == true)
        {
            if (collision.tag == "Enemy")
            {
                collision.GetComponent<EnemyActor>().TakeDamage(damage, false);
                collision.GetComponent<Rigidbody2D>().AddForce(new Vector2(UnityEngine.Random.Range(3, 10), UnityEngine.Random.Range(5, 8)), ForceMode2D.Impulse);
            }
        }
        
    }

    // end of the line
}
