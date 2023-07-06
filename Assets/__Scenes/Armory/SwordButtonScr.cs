using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SwordButtonScr : MonoBehaviour
{
    public Sword_SO sword;

    [SerializeField] private GameObject mechSelectMenu;

    // Start is called before the first frame update
    void Start()
    {
        mechSelectMenu = GameObject.Find("HangarManager");
    }

    public void InitButton(Sword_SO myBlade)
    {
        sword = myBlade;
        GetComponent<Image>().sprite = sword.image;
        transform.Find("Name").GetComponent<TMPro.TextMeshProUGUI>().text = sword.name;
    }

    public void SelectBlade()
    {
        mechSelectMenu.GetComponent<HangarMenuScr>().SelectNewSword(sword);
    }
}
