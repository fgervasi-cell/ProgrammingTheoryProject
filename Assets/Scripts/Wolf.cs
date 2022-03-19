using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wolf : Creature
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        audioSrc = GameObject.Find("WolfAudio").GetComponent<AudioSource>();
        MoveSpeed = 3.0f;
        AttackDamage = 5.0f;
        Points = 5.0f;
        LifePoints = 50.0f;
    }
}
