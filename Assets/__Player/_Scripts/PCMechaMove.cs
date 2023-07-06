using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CapsuleCollider2D))]
public class PCMechaMove : MonoBehaviour
{
    [SerializeField]
    private Vector2 moveInput;
    private Rigidbody2D playerController;
    [SerializeField]
    private int pilotHandling;
    [SerializeField]
    private int mechThrusters;
    [Header("Boundaries")]
    [SerializeField]
    private Vector2 screenBounds;
    [SerializeField]
    private float objWidth;
    [SerializeField]
    private float objHeight;


    // Start is called before the first frame update
    void Start()
    {
        playerController = GetComponent<Rigidbody2D>();
        playerController.gravityScale = 0;
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));        
        objWidth = transform.GetComponent<CapsuleCollider2D>().bounds.size.x / 2;
        objHeight = transform.GetComponent<CapsuleCollider2D>().bounds.size.y / 2;
    }

    // Update is called once per frame
    void Update()
    {
        GetStats();
        MoveTheMech();
    }
    private void LateUpdate()
    {
        PlayerBoundaries();
    }

    private void PlayerBoundaries()
    {
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        Vector3 viewPos = transform.position; // so we can alter the x and y coordinates
        viewPos.x = Mathf.Clamp(viewPos.x, screenBounds.x * -1 + objWidth, screenBounds.x - objWidth);
        viewPos.y = Mathf.Clamp(viewPos.y, screenBounds.y * -1 + objHeight, screenBounds.y - objHeight);
        transform.position = viewPos;
    }
    private void GetStats()
    {
        pilotHandling = GetComponent<PCControllerMecha>().pilot.handling;
        mechThrusters = GetComponent<PCControllerMecha>().mech.thrusters;
    }

    public void GetMoveInput(Vector2 input)
    {
        moveInput = input.normalized;
    }


    private void MoveTheMech()
    {
        if(GetComponent<PCMechaAttack>().attacking == false)
        {
            if (moveInput.x == 0 && moveInput.y == 0)
            {
                    playerController.velocity = new Vector2(0, 0);
            }
            else
            {
                playerController.velocity = new Vector2(moveInput.x * pilotHandling, moveInput.y * pilotHandling);
                //playerController.velocity = moveInput * pilotHandling;
            }
        }
        else
        {
            playerController.velocity = new Vector2(playerController.velocity.x, moveInput.y);
        }
    }


    // end of the line
}
