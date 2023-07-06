using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConceptaSpecialProjectile : MonoBehaviour
{
    public int damage;
    public float projSpeed;
    Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        rb.velocity = Vector2.right * projSpeed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Enemy")
        {
            collision.GetComponent<EnemyActor>().TakeDamage(damage, false);
        }
    }
}
