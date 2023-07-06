using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHomingProjectile : MonoBehaviour
{
    public int damage;
    public float projSpeed;
    Rigidbody2D rb;
    [SerializeField] Vector2 direction;
    bool countered;
    [SerializeField] private List<Transform> enemies;
    [SerializeField] private Transform target;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        countered = false;
        Destroy(gameObject, 10.5f);
        FillEnemyList();
    }

    private void FillEnemyList()
    {
        enemies.Clear();
        GameObject[] foes = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject trans in foes)
        {
            enemies.Add(trans.transform);
        }
        FindTarget();
    }

    private void FindTarget()
    {
        // int targetNum = Random.Range(0, enemies.Count + 1);
        int targetNum = Random.Range(0, enemies.Count);
        print(gameObject.name + " " + targetNum);
        target = enemies[targetNum];
    }

    private void FixedUpdate()
    {
        if (target != null)
        {
            if (countered == false)
            {
                direction = (target.position - transform.position).normalized;
                // rb.velocity = Vector2.MoveTowards(transform.position, target.position, projSpeed * Time.deltaTime);
                // rb.velocity = Vector2.MoveTowards(transform.position, target.position, projSpeed);
                // rb.MovePosition(target.transform.position * projSpeed);
                print("moving to target");
                rb.AddForce(direction);
            }
            else if (countered == true)
            {
                rb.velocity = Vector2.left * projSpeed;
            }
        }
        else
        {
            FillEnemyList();
        }
    }
    private void Update()
    {
        if (target != null)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, target.rotation, Time.deltaTime * (projSpeed * 0.1f));
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Enemy")
        {
            collision.GetComponent<EnemyActor>().TakeDamage(damage, true);
            Destroy(gameObject);
        }
        else if (collision.transform.tag == "Projectile")
        {
            CollideWithProj();
            collision.GetComponent<EnemyProjectile>().CollideWithProj();
        }
    }

    private void CollideWithProj()
    {
        // Destroy(gameObject);
    }

    private void ProjBlockd()
    {
        countered = true;
        rb.AddForce(new Vector2(Random.Range(3, 10), Random.Range(3, 10)), ForceMode2D.Impulse);
        transform.rotation = Quaternion.Euler(transform.rotation.x, 180, transform.rotation.z);
        rb.velocity = new Vector2(-rb.velocity.x, -rb.velocity.y);
    }
}
