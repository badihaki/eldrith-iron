using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmorPickupScr : MonoBehaviour
{
    [SerializeField] private int armorGainAmnt;
    [SerializeField] private float lifeTime;

    [SerializeField] private float speed;
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (lifeTime <= 0)
        {
            lifeTime = 5.50f;
        }
        Destroy(this.gameObject, lifeTime);
    }

    private void Update()
    {
        rb.velocity = new Vector2(-speed, rb.velocity.y);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.GetComponent<PCControllerMecha>().GetArmor(armorGainAmnt);
            Destroy(gameObject);
        }
    }
}
