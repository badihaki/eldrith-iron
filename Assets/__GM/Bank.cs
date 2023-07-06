using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bank : MonoBehaviour
{
    public int goldInBank;

    public void AddMoney(int moneyToAdd)
    {
        goldInBank += moneyToAdd;
    }
}
