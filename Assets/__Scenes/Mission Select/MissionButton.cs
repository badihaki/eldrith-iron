using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MissionButton : MonoBehaviour
{
    [Header("Mission Parameters")]
    [SerializeField] private MissionSO thisMission;
    [SerializeField] private int missionSceneIndex;
    private Button thisButton;
    [Header("Images")]
    [SerializeField] private Sprite regularImage;
    [SerializeField] private Sprite cleardSprite;
    [SerializeField] private Sprite lockedSprite;
    [Header("The next places to unlock")]
    public MissionSO[] unlockableMissions; // Put the SO for the next mission here



    // Start is called before the first frame update
    void Start()
    {
        thisButton = GetComponent<Button>();
    }

    // Update is called once per frame
    void Update()
    {
        if (thisMission.missionUnlocked != true) // if the mission isn't unlocked
        {
            // we lock it here and in SelectThisLevel()
            GetComponent<Image>().sprite = lockedSprite;
        }
        else
        {
            if (thisMission.missionComplete != true)
            {
                GetComponent<Image>().sprite = regularImage;
            }
            else
            {
                GetComponent<Image>().sprite = cleardSprite;
            }
        }
    }

    public void SelectThisLevel()
    {
        if (thisMission.missionUnlocked == true)
        {
            // play select ping        
            GameObject.Find("Canvas - Mission Select").GetComponent<MissionSelectManager>().ShowSortieScreen(thisMission, missionSceneIndex);
        }
        else
        {
            /*
             * play non-selectable ping
             */
        }
    }
}
