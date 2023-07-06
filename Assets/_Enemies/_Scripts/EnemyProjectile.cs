using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    public int damage;
    public float projSpeed;
    Rigidbody2D rb;
    bool countered;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        countered = false;
        Destroy(gameObject, 5f);
    }
    private void Update()
    {
        if (countered == false)
        {
            rb.velocity = Vector2.left * projSpeed;
        }
        else if (countered == true)
        {
            rb.velocity = Vector2.right * projSpeed;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (countered == false)
        {
            if (collision.transform.tag == "Player")
            {
                collision.GetComponent<PCControllerMecha>().TakeDamage(damage);
                Destroy(gameObject);
            }
            else if(collision.transform.tag == "Buddy")
            {
                collision.GetComponent<WeaponPickupScript>().TakeDamage(damage);
                collision.GetComponent<Rigidbody2D>().AddForce(new Vector2(UnityEngine.Random.Range(-2, 3.5f), UnityEngine.Random.Range(-2, 2)), ForceMode2D.Impulse);
            }
        }
        else if (countered == true)
        {
            if (collision.transform.tag == "Enemy")
            {
                collision.GetComponent<EnemyActor>().TakeDamage(damage, false);
                Destroy(gameObject);
            }
            else if (collision.transform.tag == "Buddy")
            {
                collision.GetComponent<WeaponPickupScript>().TakeDamage(damage);
                collision.GetComponent<Rigidbody2D>().AddForce(new Vector2(UnityEngine.Random.Range(-2, 3.5f), UnityEngine.Random.Range(-2, 2)), ForceMode2D.Impulse);
            }
        }
        
    }

    public void ProjBlockd()
    {
        rb.AddForce(new Vector2(Random.Range(3, 10), Random.Range(3, 10)), ForceMode2D.Impulse);
        rb.velocity = new Vector2(-rb.velocity.x, -rb.velocity.y);
        transform.rotation = Quaternion.Euler(transform.rotation.x, 180, transform.rotation.z);
        countered = true;
    }
    public void CollideWithProj()
    {
        Destroy(gameObject);
    }
}
