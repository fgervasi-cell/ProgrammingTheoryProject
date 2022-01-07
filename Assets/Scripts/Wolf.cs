using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wolf : Creature
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        MoveSpeed = 3.0f;
    }
}
