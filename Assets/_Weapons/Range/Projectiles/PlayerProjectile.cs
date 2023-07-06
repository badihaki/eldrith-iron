using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(CapsuleCollider2D))]
public class PlayerProjectile : MonoBehaviour
{
    public int damage;
    public float projSpeed;
    Rigidbody2D rb;
    bool countered;
    [SerializeField] private float projDestroyTime = 5.5f;
    [SerializeField] private bool isProjInvincible = false; // use if proj has full life
    [SerializeField] private GameObject hitFX;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        GetComponent<CapsuleCollider2D>().isTrigger = true;
        countered = false;
        Destroy(gameObject, projDestroyTime);
    }
    private void Update()
    {
        if (countered == false)
        {
            rb.velocity = Vector2.right * projSpeed;
        }
        else if (countered == true)
        {
            rb.velocity = Vector2.left * projSpeed;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Enemy")
        {
            collision.GetComponent<EnemyActor>().TakeDamage(damage, true);
            if (hitFX != null)
            {
                GameObject hitClone = Instantiate(hitFX, collision.transform.position, Quaternion.identity);
                Destroy(hitClone, 1.0f);
            }
            if (isProjInvincible == false)
            {
                Destroy(gameObject);
            }
        }
        else if (collision.transform.tag == "Projectile")
        {
            if (isProjInvincible == false)
            {
                collision.GetComponent<EnemyProjectile>().CollideWithProj();
                CollideWithProj();
            }
            
        }
    }

    private void CollideWithProj()
    {
        Destroy(gameObject);
    }

    private void ProjBlockd()
    {
        countered = true;
        rb.AddForce(new Vector2(Random.Range(3, 10), Random.Range(3, 10)), ForceMode2D.Impulse);
        transform.rotation = Quaternion.Euler(transform.rotation.x, 180, transform.rotation.z);
        rb.velocity = new Vector2(-rb.velocity.x, -rb.velocity.y);
    }
}
