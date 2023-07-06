using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Library_Anubis : MonoBehaviour
{
    [SerializeField]private GameObject specialProj;
    [SerializeField] private GameObject specielfx;

    private void Awake()
    {
        specielfx.SetActive(false);
    }
    public void SpecialAttackAnim()
    {
        GameObject specProjClone = Instantiate(specialProj, transform.Find("WeaponPoint").position, Quaternion.identity);
        specProjClone.GetComponent<AnubisSpecProj>().damage = GetComponent<PCControllerMecha>().mech.damageOutput * 5; // set damage
        specProjClone.GetComponent<AnubisSpecProj>().projSpeed = GetComponent<PCControllerMecha>().pilot.handling * 0.75f; // set speed
        Destroy(specProjClone, 5.5f);
    }

    public void AnimSpecFXStart()
    {
        specielfx.SetActive(true);

    }
    public void AnimSpecFXFinish()
    {
        specielfx.SetActive(false);

    }
    public void AnimSpecial()
    {
        GetComponent<PCControllerMecha>().inCutscene = true;
    }
    public void AnimEndSpec()
    {
        GetComponent<PCControllerMecha>().inCutscene = false;
    }


}
