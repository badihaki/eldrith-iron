using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLoot : MonoBehaviour
{
    [Header("Gameobjects to spawn as loot")]
    [SerializeField] private GameObject[] possibleLoot;
    [Header("The chances for each item to spawn")]
    [SerializeField] private int[] itemChance;
    private int possibleTotalNumber = 100;
    [Header("The loot's index, and the actual drop item")]
    [SerializeField] private int randomLootNumber;
    [SerializeField] private GameObject itemToDrop;

    // Start is called before the first frame update
    void Start()
    {
        DetermineLootToDrop();
    }

    private void DetermineLootToDrop()
    {
        /*
         * Ok, ok ok ok
         * We gotta set it up where we determine what loot to drop
         * Each item will have an item percentage
         */
        randomLootNumber = UnityEngine.Random.Range(0, possibleTotalNumber);
        
        for(int i = 0; i < itemChance.Length; i++)
        {
            if (randomLootNumber <= itemChance[i])
            {
                itemToDrop = possibleLoot[i];
                return;
            }
            else
            {
                randomLootNumber -= itemChance[i];
            }
        }
        /*
        foreach(var weight in itemChance)
        {
            if (randomLootNumber < weight)
            {
                // set awarded item to that item
                print("you get item " + weight);
            }
            else
            {
                randomLootNumber -= weight;
            }
        }
        */
    }

    public void DropItem()
    {
        if (itemToDrop != null)
        {
            Instantiate(itemToDrop, transform.position, Quaternion.identity);
        }
    }
}
