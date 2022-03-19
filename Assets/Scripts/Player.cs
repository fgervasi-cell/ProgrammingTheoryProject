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
    private float score = 0.0f;
    private float movePrecision = 0.1f;
    private float nextAttack = 0.0f;
    public GameObject waypointIndicator;
    public float Score
    {
        get
        {
            return score;
        }

        set
        {
            if (value > score)
            {
                score = value;
            }
            else
            {
                Debug.LogWarning("Can only increment score!");
            }
        }
    }

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        // Set the target so it is not null
        target = transform.position;
        LifePoints = 100.0f;
        AttackDamage = 15.0f;
        audioSrc = GameObject.Find("PlayerAudio").GetComponent<AudioSource>();
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

        if (targetIsEnemy && !isMoving && targetObject != null && targetObject.LifePoints > 0.0f)
        {
            anim.SetBool("isInRange", true);
            if (Time.time >= nextAttack)
            {
                Debug.Log("Attack!");
                Attack(targetObject);
                nextAttack = Time.time + anim.GetCurrentAnimatorStateInfo(0).length;
            }
        }

        if (isMoving)
        {
            Move(target, playerRot, movePrecision);
        }
        else
        {
            waypointIndicator.GetComponent<MeshRenderer>().enabled = false;
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
                waypointIndicator.GetComponent<MeshRenderer>().enabled = false;
                movePrecision = 2.0f;
                hit.transform.gameObject.TryGetComponent(out targetObject);
                targetObject.isTarget = true;
            }
            else
            {
                targetIsEnemy = false;
                movePrecision = 0.1f;
                anim.SetBool("isInRange", false);
                if (targetObject != null)
                {
                    targetObject.isTarget = false;
                }
            }
            isMoving = true;

            if (waypointIndicator != null && !targetIsEnemy)
            {
                waypointIndicator.transform.position = new Vector3(hit.point.x, 0.1f, hit.point.z);
                waypointIndicator.GetComponent<MeshRenderer>().enabled = true;
            }
        }
    }
}
