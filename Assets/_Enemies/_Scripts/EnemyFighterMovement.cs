using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFighterMovement : MonoBehaviour
{
    public bool init = false;
    [SerializeField]
    private float xSpeed;
    private Rigidbody2D myRigidbody;
    [SerializeField] private Vector2 screenBounds;
    [SerializeField] private float ySpeed;
    [SerializeField] float flipTime;
    // InitializeMovement replaces Start, as is done if needed
    public void InitializeMovement(Enemy_SO enemy)
    {
        init = true;
        xSpeed = enemy.speed * 0.65f;
        ySpeed = enemy.speed * 0.5f;
        myRigidbody = GetComponent<Rigidbody2D>();
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
    }

    // Update is called once per frame
    void Update()
    {
        FlipTimer();
        myRigidbody.velocity = new Vector2(-xSpeed, ySpeed);
        if (transform.position.y > screenBounds.y || transform.position.y < -screenBounds.y)
        {
            if (flipTime <= 0)
            {
                print("flippin the enemy");
                FlipSpeed();
                flipTime = 0.75f;
            }
        }

    }

    void FlipTimer()
    {
        if (flipTime > 0)
        {
            flipTime -= Time.deltaTime;
        }
        else if (flipTime < 0)
        {
            flipTime = 0;
        }
    }
    void FlipSpeed()
    {
        ySpeed *= -1;
        
    }
}
