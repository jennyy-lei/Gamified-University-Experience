using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goose : Enemy2,IDashable
{
    [field: SerializeField]
    public float dashDist {get;set;}
    [field: SerializeField]
    public float dashSpeed{get;set;}
}
