using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.Events;

public class MechHitbox : MonoBehaviour
{
    [SerializeField] private CinemachineImpulseSource camShaker;
    private void Awake()
    {
        camShaker = GetComponent<CinemachineImpulseSource>();
    }
    private void OnEnable()
    {
        if (camShaker == null)
        {
            camShaker = GetComponent<CinemachineImpulseSource>();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            // print("hit enemy");
            collision.GetComponent<EnemyActor>().TakeDamage(transform.parent.GetComponent<PCControllerMecha>().weapon.damage + transform.parent.GetComponent<PCControllerMecha>().mech.damageOutput, true);
            if (GetComponentInParent<PCMechaAttack>()._sword.slashEffect != null)
            {
                GameObject weaponFx = Instantiate(GetComponentInParent<PCMechaAttack>()._sword.slashEffect, collision.transform.position, Quaternion.identity);
                weaponFx.GetComponent<AudioSource>().clip = GetComponentInParent<PCMechaAttack>()._sword.hitSoundFX;
                weaponFx.GetComponent<AudioSource>().Play();
                Destroy(weaponFx, 1.0f);
            }            
            camShaker.GenerateImpulse();
            GameObject.Find("Game Master").GetComponent<GMTimeManager>().HitStop(0.105f);
        }
        else if (collision.tag == "Projectile")
        {
            // print("hit proj");
            collision.GetComponent<EnemyProjectile>().ProjBlockd();
            camShaker.GenerateImpulse();
            GameObject.Find("Game Master").GetComponent<GMTimeManager>().HitStop(0.085f);
        }
    }
}
