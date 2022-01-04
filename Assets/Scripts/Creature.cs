using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creature : Character
{
    public float lifePoints = 15.0f;
    private float speed = 5.0f;
    private GameObject player;

    public override void Attack()
    {
        throw new System.NotImplementedException();
    }

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (lifePoints <= 0)
        {
            Die();
        }

        if (Vector3.Distance(transform.position, player.transform.position) < 10.0f)
        {
            Move();
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
    }

}
