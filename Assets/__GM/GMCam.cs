using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class GMCam : MonoBehaviour
{
    private void Awake()
    {
        CinemachineVirtualCamera vCam = GetComponent<CinemachineVirtualCamera>();
        vCam.Follow = GameObject.Find("Game Master").transform;
        vCam.LookAt = GameObject.Find("Game Master").transform;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
