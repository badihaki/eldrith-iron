using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Library_Nusuit : MonoBehaviour
{

    bool special;
    Rigidbody2D rb;
    [SerializeField] float flipTime;
    [SerializeField] float xSpeed = 6f;
    [SerializeField] float ySpeed = 6f;
    [SerializeField] float buffer;
    [SerializeField] private Vector2 screenBounds;

    [SerializeField] private GameObject specialgfx;
    // Start is called before the first frame update
    void Start()
    {
        special = false;
        rb = GetComponent<Rigidbody2D>();
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        specialgfx = transform.Find("Special").gameObject; specialgfx.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (special == true)
        {
            if (specialgfx.activeSelf != true) specialgfx.SetActive(true);
            if (flipTime > 0)
            {
                flipTime -= Time.deltaTime;
            }
            rb.velocity = new Vector2(xSpeed, ySpeed);
            ControlMove();
        }
        else if (special == false)
        {
            if (specialgfx.activeSelf != false) specialgfx.SetActive(false);
        }
    }

    public void AnimSpecial()
    {
        special = true;
        GetComponent<PCControllerMecha>().inCutscene = true;
    }
    public void AnimEndSpec()
    {
        special = false;
        GetComponent<PCControllerMecha>().inCutscene = false;
        transform.eulerAngles = new Vector3(transform.rotation.x, 0, transform.rotation.y);
        if (xSpeed < 0) xSpeed *= -1;
        if (ySpeed < 0) ySpeed *= -1;
    }
    private void ControlMove()
    {
        if (flipTime <= 0)
        {
            if (transform.position.y >= screenBounds.y - buffer || transform.position.y <= -screenBounds.y + buffer)
            {
                ySpeed *= -1;
            }
            if (transform.position.x >= screenBounds.x - buffer || transform.position.x <= -screenBounds.x + buffer)
            {
                xSpeed *= -1;
                if (transform.rotation.y == 0)
                {
                    transform.eulerAngles = new Vector3(transform.rotation.x, 180, transform.rotation.y);
                }
                else
                {
                    transform.eulerAngles = new Vector3(transform.rotation.x, 0, transform.rotation.y);
                }
            }
            flipTime = 0.15f;
        }

    }
}
