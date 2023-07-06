using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Library_Gigadren : MonoBehaviour
{
    public bool special;

    [Header("Projectile Vars")]
    [SerializeField] GameObject projectile;
    [SerializeField] Transform shootPoint;
    
    [Header("Managers")]
    [SerializeField] float atkTime;
    public float timeToAddToTimer;
    [SerializeField] int damage;
    [SerializeField] int currentShootCount;
    [SerializeField] int totalShootCount;
    // Start is called before the first frame update
    void Start()
    {
        currentShootCount = totalShootCount;
    }

    // Update is called once per frame
    void Update()
    {
        if (special == true)
        {
            ShootProj();
            AtkTimer();
        }
        else
        {
            atkTime = timeToAddToTimer;
        }
    }

    public void AnimSpecial() 
    {
        special = true;
    }
    public void AnimEndSpec()
    {
        special = false;
        currentShootCount = totalShootCount;
    }

    private void ShootProj()
    {
        // if (currentShootCount <= 0) currentShootCount = totalShootCount;
        if (atkTime <= 0)
        {
            print("shooting the special - gigadren");
            if (currentShootCount >= 0)
            {
                GameObject proj = Instantiate(projectile, shootPoint.position, shootPoint.rotation) as GameObject;
                proj.GetComponent<PlayerHomingProjectile>().damage = damage;
                proj.GetComponent<PlayerHomingProjectile>().projSpeed = 5.5f;
                currentShootCount--;
                atkTime = timeToAddToTimer;
                return;
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

}
