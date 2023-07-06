using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MechButtonScr : MonoBehaviour
{
    public Mecha_SO mecha;

    [SerializeField] private GameObject mechSelectMenu;

    // Start is called before the first frame update
    void Start()
    {
        mechSelectMenu = GameObject.Find("HangarManager");
    }

    public void InitButton(Mecha_SO myMech)
    {
        mecha = myMech;
        GetComponent<Image>().sprite = mecha.portrait;
        transform.Find("Name").GetComponent<TMPro.TextMeshProUGUI>().text = mecha.name;
    }

    public void SelectMech()
    {
        mechSelectMenu.GetComponent<HangarMenuScr>().SelectNewMecha(mecha);
    }
}
