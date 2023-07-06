using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private int damage;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    internal void InitializeEnemyAttack(Enemy_SO enemy)
    {
        damage = enemy.damage;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            collision.GetComponent<PCControllerMecha>().TakeDamage(damage);
            Destroy(this.gameObject);
        }
        else if (collision.transform.tag == "Buddy")
        {
            collision.GetComponent<WeaponPickupScript>().TakeDamage(damage);
            collision.GetComponent<Rigidbody2D>().AddForce(new Vector2(UnityEngine.Random.Range(-2, 3.5f), UnityEngine.Random.Range(-2, 2)), ForceMode2D.Impulse);
        }
    }
}
