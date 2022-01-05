using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creature : Character
{
    public float lifePoints = 15.0f;
    private float attackDamage = 0.0f;
    private float speed = 1.0f;
    private Player player;
    private Animator anim;

    public override void Attack()
    {
        player.lifePoints -= attackDamage;
        transform.LookAt(new Vector3(player.transform.position.x, 0.0f, player.transform.position.z));
    }

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
        anim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (lifePoints <= 0)
        {
            Die();
        }

        if (player.lifePoints < 0)
        {
            CancelInvoke();
            anim.SetBool("isInRange", false);
        }
        else if (Vector3.Distance(transform.position, player.transform.position) < 10.0f
            && Vector3.Distance(transform.position, player.transform.position) >= 1.0f)
        {
            CancelInvoke();
            anim.SetBool("isInRange", false);
            Move();
        }
        else if (Vector3.Distance(transform.position, player.transform.position) <= 1.0f)
        {
            anim.SetBool("isMoving", false);
            if (!anim.GetBool("isInRange"))
            {
                InvokeRepeating("Attack", anim.GetCurrentAnimatorStateInfo(0).length, anim.GetCurrentAnimatorStateInfo(0).length);
            }
            anim.SetBool("isInRange", true);
        }
    }

    private void Die()
    {
        FindObjectOfType<Player>().targetIsEnemy = false;
        Destroy(gameObject);
    }

    public override void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
        Vector3 direction = (player.transform.position - transform.position).normalized;
        Quaternion rotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 5.0f);
        anim.SetBool("isMoving", true);
    }

}
