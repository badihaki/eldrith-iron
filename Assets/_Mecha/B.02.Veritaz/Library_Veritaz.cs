using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Library_Veritaz : MonoBehaviour
{
    [SerializeField] private GameObject specialProj;
    [SerializeField] private GameObject finalSpecProj;
    [SerializeField] private GameObject specielfx;

    private void Awake()
    {
        specielfx.SetActive(false);
    }
    public void SpecialAttackAnim()
    {
        GameObject specProjClone = Instantiate(specialProj, transform.Find("WeaponPoint").position, Quaternion.identity);
        specProjClone.GetComponent<PlayerProjectile>().damage = GetComponent<PCControllerMecha>().mech.damageOutput * 2; // set damage
        specProjClone.GetComponent<PlayerProjectile>().projSpeed = GetComponent<PCControllerMecha>().pilot.handling * 1.75f; // set speed
        Destroy(specProjClone, 5.5f);
    }
    public void FinisherSpecialAttackAnim()
    {
        GameObject specProjClone = Instantiate(finalSpecProj, transform.Find("WeaponPoint").position, Quaternion.identity);
        specProjClone.GetComponent<PlayerProjectile>().damage = GetComponent<PCControllerMecha>().mech.damageOutput * 3; // set damage
        specProjClone.GetComponent<PlayerProjectile>().projSpeed = GetComponent<PCControllerMecha>().pilot.handling * 0.75f; // set speed
        Destroy(specProjClone, 2.5f);
    }

    public void AnimSpecFXStart()
    {
        specielfx.SetActive(true);
        GetComponent<Rigidbody2D>().AddForce(Vector2.zero);
    }
    public void AnimSpecFXFinish()
    {
        specielfx.SetActive(false);
    }
    public void AnimSpecial()
    {
        GetComponent<PCControllerMecha>().inCutscene = true;
        // GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        GetComponent<Rigidbody2D>().AddForce(Vector2.zero);

    }
    public void AnimEndSpec()
    {
        GetComponent<PCControllerMecha>().inCutscene = false;
    }
}
