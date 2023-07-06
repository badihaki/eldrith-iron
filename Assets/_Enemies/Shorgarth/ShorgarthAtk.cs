using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShorgarthAtk : MonoBehaviour
{
    [SerializeField]GameObject projectile;
    [SerializeField] Transform shootPoint;
    [SerializeField] Enemy_SO enemyStatSheet;
    [SerializeField] float atkTime;
    public float timeToAddToTimer;
    [SerializeField] int damage;
    [SerializeField] int currentShootCount;
    [SerializeField] int totalShootCount;

    // Start is called before the first frame update
    void Start()
    {
        damage = enemyStatSheet.damage;
        atkTime = timeToAddToTimer * .25f;
    }

    // Update is called once per frame
    void Update()
    {
        AtkTimer();
        ShootProj();
    }

    private void ShootProj()
    {
        if (currentShootCount <= 0) currentShootCount = totalShootCount;
        if (atkTime == 0)
        {
            if (currentShootCount > 1)
            {
                GameObject proj = Instantiate(projectile, shootPoint.position, shootPoint.rotation)as GameObject;
                proj.GetComponent<EnemyProjectile>().damage = damage;
                currentShootCount--;
                atkTime = timeToAddToTimer * .025f;
                return;
            }
            else if (currentShootCount == 1)
            {
                GameObject proj = Instantiate(projectile, shootPoint.position, shootPoint.rotation) as GameObject;
                proj.GetComponent<EnemyProjectile>().damage = damage;
                currentShootCount = totalShootCount;
                atkTime = timeToAddToTimer;
            }
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

    // end of the line
}
