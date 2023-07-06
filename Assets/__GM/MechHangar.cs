using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechHangar : MonoBehaviour
{
    public List<Mecha_SO> mechList;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void AddNewMechaToHangar(Mecha_SO newRig)
    {
        if (mechList.IndexOf(newRig) < 0) // if the object isn't found, this should return -1
        {
            mechList.Add(newRig);
        }
    }
}
