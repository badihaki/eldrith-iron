using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MissionSelectManager : MonoBehaviour
{
    [Header("Screen Setup")]
    [SerializeField] private GameObject sortieScreen;
    [SerializeField] private GameObject[] missionScreens;
    [SerializeField] private int currentScreen = 0;

    [Header("Screen Locations")]
    [SerializeField] private Transform backStack;
    [SerializeField] private Transform frontStack;
    [SerializeField] private Transform currentScreenLoc;

    [Header("Buttons")]
    [SerializeField] private GameObject backButton;
    [SerializeField] private GameObject frontButton;

    [Header("MISC")]
    private int missionIndexNumber;

    // Start is called before the first frame update
    void Start()
    {
        foreach(GameObject screen in missionScreens)
        {
            screen.transform.position = frontStack.position;
        }
        missionScreens[0].transform.position = currentScreenLoc.position;
        currentScreen = 0;
        sortieScreen.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (sortieScreen.activeSelf == false)
        {

            // if on the first screen (screen 0) you cant go back
            if (currentScreen <= 0)
            {
                backButton.SetActive(false);
            }
            else if (currentScreen > 0)
            {
                backButton.SetActive(true);
            }
            // if on the last screen, you cant go forward
            if (currentScreen >= missionScreens.Length - 1)
            {
                frontButton.SetActive(false);
            }
            else if (currentScreen < missionScreens.Length - 1)
            {
                frontButton.SetActive(true);
            }
        }
    }

    public void BackButton()
    {
        // move the current screen to the forward stack
        missionScreens[currentScreen].transform.position = frontStack.position;

        currentScreen -= 1;
        missionScreens[currentScreen].transform.position = currentScreenLoc.position;
    }

    public void StartMissionButton()
    {
        GameObject.Find("Game Master").GetComponent<GM>().StartLevelByIndex(missionIndexNumber);
    }

    public void FrontButton()
    {
        // move the current screen to the back stack
        missionScreens[currentScreen].transform.position = backStack.position;

        currentScreen += 1;
        missionScreens[currentScreen].transform.position = currentScreenLoc.position;
    }

    public void GoHome()
    {
        GameObject.Find("Game Master").GetComponent<GM>().StartLevelByIndex(0);
    }

    public void ShowSortieScreen(MissionSO missionToLoad, int missionIndex)
    {
        sortieScreen.SetActive(true);
        missionScreens[currentScreen].SetActive(false);
        backButton.SetActive(false);
        frontButton.SetActive(false);
        sortieScreen.transform.Find("Title").GetComponent<TMPro.TextMeshProUGUI>().text = missionToLoad.missionTitle;
        sortieScreen.transform.Find("Info").GetComponent<TMPro.TextMeshProUGUI>().text = missionToLoad.missionInfo;
        sortieScreen.transform.Find("Image").GetComponent<Image>().sprite = missionToLoad.openingImage;

        missionIndexNumber = missionIndex;
    }
    public void FuckThatSortieScreen()
    {
        sortieScreen.SetActive(false);
        missionScreens[currentScreen].SetActive(true);
        backButton.SetActive(true);
        frontButton.SetActive(true);

        missionIndexNumber = 0;
    }
}
