using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GMTimeManager : MonoBehaviour
{
    [SerializeField] private bool waiting;

    private void Start()
    {
        waiting = false;
    }
    public void HitStop(float duration)
    {
        if (waiting) return;
        else
        {
            StartCoroutine(Wait(duration));
        }
        
    }

    IEnumerator Wait(float duration)
    {
        waiting = true;
        Time.timeScale = 0.0f;
        yield return new WaitForSecondsRealtime(duration);
        Time.timeScale = 1.0f;
        waiting = false;
    }
}
