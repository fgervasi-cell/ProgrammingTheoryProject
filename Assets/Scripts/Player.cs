using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    private readonly float speed = 5.0f;
    private Vector3 target;
    private Quaternion playerRot;
    private bool isMoving = false;
    public bool targetIsEnemy = false;
    private float attackDamage = 5.0f;
    private float attackSpeed = 2.0f;
    public GameObject avatar;
    private Animator anim;
    private BoxCollider playerCollider;
    private Creature targetObject;

    public override void Attack()
    {
        targetObject.lifePoints -= attackDamage;
        if (targetObject.lifePoints < 0)
        {
            CancelInvoke();
            anim.SetBool("isInRange", false);
            anim.SetBool("isMoving", false);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        target = transform.position;
        anim = avatar.GetComponent<Animator>();
        playerCollider = avatar.GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(1))
        {
            SetTargetPosition();
        }

        if (Vector3.Distance(transform.position, target) < 1.5f && targetIsEnemy)
        {
            isMoving = false;
            if (!anim.GetBool("isInRange"))
            {
                InvokeRepeating("Attack", anim.GetCurrentAnimatorStateInfo(0).length, anim.GetCurrentAnimatorStateInfo(0).length);
            }
            anim.SetBool("isInRange", true);
        }

        if (isMoving)
        {
            Move();
        }
    }

    public override void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(target.x, transform.position.y, target.z), Time.deltaTime * speed);
        transform.rotation = Quaternion.Slerp(transform.rotation, playerRot, Time.deltaTime * speed);

        if (Mathf.Abs(Vector3.Distance(transform.position, target)) < 0.5f)
        {
            isMoving = false;
        }
        anim.SetBool("isMoving", isMoving);
    }

    private void SetTargetPosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        targetIsEnemy = false;

        if (Physics.Raycast(ray, out RaycastHit hit, 1000))
        {
            target = hit.point;
            hit.transform.gameObject.TryGetComponent(out targetObject);
            Vector3 lookAtTarget = new Vector3(target.x - transform.position.x, 0.0f, target.z - transform.position.z);
            playerRot = Quaternion.LookRotation(lookAtTarget);

            if (hit.collider.gameObject.CompareTag("Enemy"))
            {
                targetIsEnemy = true;
            }
            isMoving = true;
        }
    }
}
