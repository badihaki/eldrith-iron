using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFormation : MonoBehaviour
{
    public float formTimeCost;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 25.5f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
