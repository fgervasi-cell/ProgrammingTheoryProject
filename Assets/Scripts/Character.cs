using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    protected Animator anim;
    [SerializeField]
    private GameObject avatar;
    protected bool isMoving = false;
    private float moveSpeed = 5.0f;
    public float MoveSpeed
    {
        get
        {
            return moveSpeed;
        }

        set
        {
            if (value > 0.0f)
            {
                moveSpeed = value;
            }
            else
            {
                Debug.LogWarning("Cannto set MoveSpeed to a negative value!");
            }
        }
    }
    private float rotSpeed = 5.0f;
    public float RotSpeed
    {
        get
        {
            return rotSpeed;
        }

        set
        {
            if (value > 0.0f)
            {
                rotSpeed = value;
            }
            else
            {
                Debug.LogWarning("Cannot set RotSpeed to a negative value!");
            }
        }
    }
    private float lifePoints = 20.0f;
    public float LifePoints
    {
        get
        {
            return lifePoints;
        }

        set
        {
            if (value > 0.0f)
            {
                lifePoints = value;
            }
            else
            {
                Debug.LogWarning("Cannot set LifePoints to a negative value!");
            }
        }
    }
    private float attackDamage = 5.0f;
    public float AttackDamage
    {
        get
        {
            return attackDamage;
        }

        set
        {
            if (value > 0.0f)
            {
                attackDamage = value;
            }
            else
            {
                Debug.LogWarning("Cannot set AttackDamage to a negative value!");
            }
        }
    }

    protected virtual void Start()
    {
        anim = avatar.GetComponent<Animator>();
    }

    protected virtual void Update()
    {
        if (lifePoints < 0)
        {
            Die();
        }
    }

    protected void Attack(Character character)
    {
        character.lifePoints -= attackDamage;
        transform.LookAt(new Vector3(character.transform.position.x, 0.0f, character.transform.position.z));
        if (character.LifePoints < 0)
        {
            CancelInvoke();
            anim.SetBool("isInRange", false);
            anim.SetBool("isMoving", false);
        }
    }

    protected void Move(Vector3 target, Quaternion rotation)
    {
        if (Mathf.Abs(Vector3.Distance(transform.position, target)) < 1.0f)
        {
            isMoving = false;
        }

        transform.position = Vector3.MoveTowards(transform.position, target, moveSpeed * Time.deltaTime);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotSpeed * Time.deltaTime);
        anim.SetBool("isMoving", isMoving);
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
