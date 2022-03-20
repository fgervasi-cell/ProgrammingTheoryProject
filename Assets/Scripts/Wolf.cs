using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wolf : Creature
{
    [SerializeField] private AudioClip wolfHowl;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        MoveSpeed = 3.0f;
        AttackDamage = 5.0f;
        Points = 5.0f;
        LifePoints = 50.0f;
    }
}
