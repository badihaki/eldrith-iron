using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Enemy",menuName ="Actor/Enemy")]
public class Enemy_SO : ScriptableObject
{
    [Header("Essential Info")]
    public string enemyName;
    public enum EnemyClassType
    {
        kamikaze,
        fighter,
        boss,
    }
    public EnemyClassType enemyType;
    [Header("Stats")]
    public int health;
    public float speed;
    public int damage;
    public int expGiven;
}
