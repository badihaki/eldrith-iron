using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyExplode : MonoBehaviour
{
    public GameObject[] bodyParts;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ExplodeEnemy()
    {
        foreach(GameObject part in bodyParts)
        {
            GameObject cloneBody = Instantiate(part, transform.position, Quaternion.identity);
            cloneBody.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-3, 3), Random.Range(-3, 3)), ForceMode2D.Impulse);
            Destroy(cloneBody, 1.150f);
        }
    }
}
