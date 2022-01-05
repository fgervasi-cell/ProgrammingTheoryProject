using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    private Vector3 target;
    private Quaternion playerRot;
    public bool targetIsEnemy = false;
    private Creature targetObject;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        target = transform.position;
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        if (Input.GetMouseButton(1))
        {
            SetTargetPosition();
        }

        if (Vector3.Distance(transform.position, target) < 1.5f && targetIsEnemy)
        {
            isMoving = false;
            if (!anim.GetBool("isInRange"))
            {
                InvokeRepeating("DealDamage", anim.GetCurrentAnimatorStateInfo(0).length, anim.GetCurrentAnimatorStateInfo(0).length);
            }
            anim.SetBool("isInRange", true);
        }

        if (!targetIsEnemy)
        {
            anim.SetBool("isInRange", false);
            CancelInvoke();
        }

        if (isMoving)
        {
            Move(target, playerRot);
        }
    }

    private void SetTargetPosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        targetIsEnemy = false;

        if (Physics.Raycast(ray, out RaycastHit hit, 1000))
        {
            target = hit.point;
            Vector3 lookAtTarget = new Vector3(target.x - transform.position.x, 0.0f, target.z - transform.position.z);
            playerRot = Quaternion.LookRotation(lookAtTarget);

            if (hit.collider.gameObject.CompareTag("Enemy"))
            {
                targetIsEnemy = true;
                hit.transform.gameObject.TryGetComponent(out targetObject);
                if (Vector3.Distance(transform.position, target) < 1.5f)
                {
                    transform.LookAt(new Vector3(target.x, 0.0f, target.z));
                }
            }
            isMoving = true;
        }
    }

    private void DealDamage()
    {
        Attack(targetObject);
    }
}
