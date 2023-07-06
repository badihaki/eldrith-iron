using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScreenScr : MonoBehaviour
{
    [SerializeField] private GM GM;
    // Start is called before the first frame update
    void Start()
    {
        GM = GameObject.Find("Game Master").GetComponent<GM>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LaunchSceneByIndex(int indexNo)
    {
        GM.StartLevelByIndex(indexNo);
    }

    public void LaunchSceneByName(string lvlName)
    {
        GM.StartLevelByName(lvlName);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    // end of the line
}
