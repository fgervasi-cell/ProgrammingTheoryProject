using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Represents an enemy character which follows and attacks the player if it is in a certain range.
/// </summary>
public class Creature : Character
{
    private Player player;
    [SerializeField]
    private GameObject targetIndicator;
    public bool isTarget = false;

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

        // Start moving towards the player if he is in a certain range
        // Make sure that the max distance specified here ist the opposite of the distance specified in the Move method of Character!!!
        if (Vector3.Distance(transform.position, player.transform.position) < 10.0f
            && Vector3.Distance(transform.position, player.transform.position) >= 1.5f)
        {
            CancelInvoke();
            anim.SetBool("isInRange", false);
            isMoving = true;
        }

        if (isMoving)
        {
            Move(player.transform.position, Quaternion.LookRotation(player.transform.position - transform.position).normalized);
        }

        if (!isMoving && !anim.GetBool("isInRange"))
        {
            InvokeRepeating("DealDamage", 0.0f, anim.GetCurrentAnimatorStateInfo(0).length * 1.5f);
            anim.SetBool("isInRange", true);
        }

        // Toggle the target indicator
        if (isTarget)
        {
            targetIndicator.GetComponent<MeshRenderer>().enabled = true;
        }
        else
        {
            targetIndicator.GetComponent<MeshRenderer>().enabled = false;
        }
    }

    private void DealDamage()
    {
        Attack(player);
    }
}
