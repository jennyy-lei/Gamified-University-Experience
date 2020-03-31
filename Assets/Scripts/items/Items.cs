using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Items : MonoBehaviour, IItem
{
    [field: SerializeField]
    public int value{get;set;}

    [field: SerializeField]
    public float dropChance{get;set;}
}

interface IItem
{
    int value{get;set;}

    float dropChance{get;set;}
}