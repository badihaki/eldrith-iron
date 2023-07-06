using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KamikazeMove : MonoBehaviour
{
    public bool init = false;
    [SerializeField]
    private float speed;
    private Rigidbody2D myRigidbody;
    [SerializeField] private Vector2 screenBounds;
    // InitializeMovement replaces Start, as is done if needed
    public void InitializeMovement(Enemy_SO enemy)
    {
        init = true;
        speed = enemy.speed;
        myRigidbody = GetComponent<Rigidbody2D>();
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
    }

    // Update is called once per frame
    void Update()
    {
        myRigidbody.velocity = new Vector2(-speed, myRigidbody.velocity.y);
    }
}
