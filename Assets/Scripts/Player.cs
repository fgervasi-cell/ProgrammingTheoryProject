using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Represents a player character.
/// Contains the logic for the player controls.
/// </summary>
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
        // Set the target so it is not null
        target = transform.position;
        LifePoints = 100.0f;
        AttackDamage = 15.0f;
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();

        // Prevent weird behavior/clipping of the player character when it tries to attack
        transform.position = new Vector3(transform.position.x, 0.0f, transform.position.z);

        // If the right mouse button is clicked set a new target
        if (Input.GetMouseButton(1))
        {
            SetTargetPosition();
        }

        if (targetIsEnemy && !isMoving && targetObject != null && !anim.GetBool("isInRange"))
        {
            InvokeRepeating("DealDamage", 0.0f, anim.GetCurrentAnimatorStateInfo(0).length * 1.5f);
            transform.LookAt(targetObject.transform);
            anim.SetBool("isInRange", true);
        }

        if (isMoving)
        {
            Move(target, playerRot);
        }
    }

    /// <summary>
    /// Responsible for determining the position where the player should move and if this target is an enemy object or not.
    /// </summary>
    private void SetTargetPosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit, 1000))
        {
            target = hit.point;
            Vector3 lookAtTarget = new Vector3(target.x - transform.position.x, 0.0f, target.z - transform.position.z);
            playerRot = Quaternion.LookRotation(lookAtTarget);

            if (hit.transform.gameObject.CompareTag("Enemy"))
            {
                targetIsEnemy = true;
                hit.transform.gameObject.TryGetComponent(out targetObject);
                targetObject.isTarget = true;
            }
            else
            {
                targetIsEnemy = false;
                anim.SetBool("isInRange", false);
                CancelInvoke();
                if (targetObject != null)
                {
                    targetObject.isTarget = false;
                }
            }
            isMoving = true;
        }
    }

    // This is only a workaround because it is not possible to use InvokeRepeating for methods with one or more arguments
    private void DealDamage()
    {
        Attack(targetObject);
    }
}
