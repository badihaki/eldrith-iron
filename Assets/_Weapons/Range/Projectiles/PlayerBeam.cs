using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBeam : MonoBehaviour
{

    public int damage;

    private void Awake()
    {
        GetComponent<BoxCollider2D>().isTrigger = true;
        damage = GetComponentInParent<PlayerProjectile>().damage;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Enemy")
        {
            collision.GetComponent<EnemyActor>().TakeDamage(damage, true);
        }
        else if (collision.transform.tag == "Projectile")
        {
            collision.GetComponent<EnemyProjectile>().CollideWithProj();
        }
    }
}
