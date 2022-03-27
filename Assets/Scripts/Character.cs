using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class should be used on all humanoid or generic characters in the game.
/// It provides all necessary utilities to move a character and let it attack.
/// The "isMoving" variable should be used to specify when a character is able to move and when not.
/// </summary>
[RequireComponent(typeof(AudioSource))]
public class Character : MonoBehaviour
{
    protected Animator anim;
    [SerializeField] private GameObject avatar;
    [SerializeField] private AudioClip dyingSound;
    [SerializeField] private AudioClip[] attackSound;
    [SerializeField] private AudioClip[] walkingSound;
    protected AudioSource audioSrc;
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
            if (value >= 0.0f)
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
        audioSrc = GetComponent<AudioSource>();
    }

    protected virtual void Update()
    {
        if (lifePoints <= 0)
        {
            Die();
        }
    }

    /// <summary>
    /// Subtracts attackDamage from the specified <paramref name="character"/>.
    /// If the <paramref name="character"/> object gets destroyed or its lifepoints fall below 0 all invocations will be interrupted and
    /// the "isInRange" parameter of the animator is set to false.
    /// </summary>
    /// <param name="character">The Gameobject of type Character that should be attacked.</param>
    protected void Attack(Character character)
    {
        if (!audioSrc.isPlaying)
        {
            audioSrc.PlayOneShot(attackSound[Random.Range(0, attackSound.Length)]);
        }
        character.lifePoints -= attackDamage;
        Vector3 rotation = new Vector3(character.transform.position.x - transform.position.x, 0.0f, character.transform.position.z - transform.position.z);
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(rotation), 100.0f * Time.deltaTime);
        if (character == null || character.LifePoints <= 0)
        {
            anim.SetBool("isInRange", false);
            return;
        }
    }

    /// <summary>
    /// Moves the character towards the <paramref name="target"/> position while executing the specified <paramref name="rotation"/>.
    /// Sets the "isMoving" parameter of the animator to true.
    /// If the distance between the object and the target gets below a certain value isMoving is set to false and no movement will occur.
    /// </summary>
    /// <param name="rotation">The rotation which should be performed while moving.</param>
    /// <param name="target">The target to move to.</param>
    protected void Move(Vector3 target, Quaternion rotation, float precision)
    {
        if (Mathf.Abs(Vector3.Distance(transform.position, target)) <= precision)
        {
            audioSrc.Stop();
            isMoving = false;
            anim.SetBool("isMoving", false);
            return;
        }
        if (!audioSrc.isPlaying)
        {
            audioSrc.PlayOneShot(walkingSound[Random.Range(0, walkingSound.Length)]);
        }
        transform.position = Vector3.MoveTowards(transform.position, target, moveSpeed * Time.deltaTime);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotSpeed * Time.deltaTime);
        anim.SetBool("isMoving", true);
    }

    /// <summary>
    /// Sets the "isDead" parameter of the animator to true if the rig of the animator is humanoid.
    /// Otherwise the GameObject will get destroyed.
    /// </summary>
    private void Die()
    {
        audioSrc.PlayOneShot(dyingSound);
        if (anim.isHuman)
        {
            anim.SetBool("isDead", true);
        }
        else
        {
            gameObject.SetActive(false);
        }
        if (GetComponent<Collider>() != null)
        {
        GetComponent<Collider>().enabled = false;
        }
        this.enabled = false;
    }
}
