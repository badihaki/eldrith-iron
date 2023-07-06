using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class PCMechaAttack : MonoBehaviour
{
    
    private Animator anim;
    public Gun_SO _gun;
    public Sword_SO _sword;
    public bool attacking; // this is actually controlled by input from the mecha controller
    public float atkTime;
    [SerializeField] private AudioSource soundSource;
    [SerializeField] private Vector3 projOffset = new Vector3(1.15f, 0f, 0f);
    private Rigidbody2D rb;
    public enum AtkType
    {
        melee,
        range,
    }
    public AtkType atkType;

    // Start is called before the first frame update
    void Start()
    {
        atkTime = 0;
        anim = GetComponent<Animator>();
        soundSource = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        AtkTimer();
        if (attacking == true && atkTime == 0)
        {
            Attack();
        }
    }

    private void AtkTimer()
    {
        if (atkTime > 0)
        {
            atkTime -= Time.deltaTime;
        }
        else if (atkTime <= 0)
        {
            atkTime = 0;
        }
    }

    public void Attack()
    {
        if (atkType == AtkType.range)
        {
            AtkRange();
        }
        else if (atkType == AtkType.melee)
        {
            AtkMelee();
        }
    }

    private void AtkMelee()
    {
        if (GetComponent<PCControllerMecha>().overHeated == false)
        {
            atkTime += _sword.atkRate;
            GetComponent<PCControllerMecha>().UseHeat();
            soundSource.clip = _sword.weaponSound;
            soundSource.Play();
            anim.SetTrigger("attack");
            rb.AddForce(_sword.weaponForce, ForceMode2D.Impulse);
        }

    }

    private void AtkRange()
    {
        if (GetComponent<PCControllerMecha>().overHeated == false)
        {
            // var proj = Instantiate(_gun.projectile, transform.Find("WeaponPoint").position, Quaternion.identity);
            var proj = Instantiate(_gun.projectile, transform.Find("WeaponPoint").position + projOffset, Quaternion.identity);

            proj.GetComponent<PlayerProjectile>().damage = _gun.damage + GetComponent<PCControllerMecha>().mech.damageOutput;
            proj.GetComponent<PlayerProjectile>().projSpeed = _gun.projSpeed;
            atkTime += _gun.atkRate;
            GetComponent<PCControllerMecha>().UseHeat();
            soundSource.clip = _gun.weaponSound;
            soundSource.Play();
            anim.SetTrigger("attack");
            rb.AddForce(_gun.weaponForce, ForceMode2D.Impulse);
        }
        
    }

    public void Special()
    {  
        GetComponent<PCControllerMecha>().UseGutsforGuts();
        atkTime += 2.5f;
        anim.SetTrigger("special");
    }
}
