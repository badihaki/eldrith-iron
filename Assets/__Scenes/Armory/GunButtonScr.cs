using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GunButtonScr : MonoBehaviour
{
    public Gun_SO gun;

    [SerializeField] private GameObject gunSelectMenu;

    // Start is called before the first frame update
    void Start()
    {
        gunSelectMenu = GameObject.Find("HangarManager");
    }

    public void InitButton(Gun_SO myGun)
    {
        gun = myGun;
        GetComponent<Image>().sprite = gun.image;
        transform.Find("Name").GetComponent<TMPro.TextMeshProUGUI>().text = gun.name;
    }

    public void SelectGun()
    {
        gunSelectMenu.GetComponent<HangarMenuScr>().SelectNewGun(gun);
    }
}
