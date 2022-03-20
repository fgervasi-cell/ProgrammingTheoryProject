using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPotion : Pickup
{
    private Player player;
    private int healthIncrementAmount = 5;

    protected override void Start()
    {
        base.Start();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    protected override void ReleaseEffect()
    {
        player.LifePoints = player.LifePoints += healthIncrementAmount;
    }
}
