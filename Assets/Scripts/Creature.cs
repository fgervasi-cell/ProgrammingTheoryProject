using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creature : Character
{
    private Player player;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        if (player.LifePoints < 0)
        {
            CancelInvoke();
            anim.SetBool("isInRange", false);
        }
        
        if (Vector3.Distance(transform.position, player.transform.position) < 10.0f
            && Vector3.Distance(transform.position, player.transform.position) >= 1.0f)
        {
            CancelInvoke();
            anim.SetBool("isInRange", false);
            isMoving = true;
            
        }
        else if (Vector3.Distance(transform.position, player.transform.position) <= 1.0f)
        {
            anim.SetBool("isMoving", false);
            if (!anim.GetBool("isInRange"))
            {
                InvokeRepeating("DealDamage", anim.GetCurrentAnimatorStateInfo(0).length, anim.GetCurrentAnimatorStateInfo(0).length);
            }
            anim.SetBool("isInRange", true);
        }

        if (isMoving)
        {
            Move(player.transform.position, Quaternion.LookRotation(player.transform.position - transform.position).normalized);
        }
    }

    private void DealDamage()
    {
        Attack(player);
    }
}
