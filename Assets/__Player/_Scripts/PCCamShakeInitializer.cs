using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.Events;

public class PCCamShakeInitializer : MonoBehaviour
{
    public UnityEvent shake;

    private void CamShakeEvent()
    {
        shake.Invoke();
    }
}
