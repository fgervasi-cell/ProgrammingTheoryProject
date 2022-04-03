using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Represents an enemy character which follows and attacks the player if it is in a certain range.
/// </summary>
public class Creature : Character
{
    private Player player;
    private RectTransform healthBarCanvas;
    private Transform mainCameraTransform;
    [SerializeField] private GameObject targetIndicator;
    public bool isTarget = false;
    private float nextAttack = 0.0f;
    private float points = 5.0f;
    public float Points
    {
        get
        {
            return points;
        }

        set
        {
            if (value > 0.0f)
            {
                points = value;
            }
            else
            {
                Debug.LogWarning("Points cannot be set to a negative value!");
            }
        }
    }

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
        healthBarCanvas = Utility.FindGameObjectWithComponentByNameInChildren<RectTransform>(gameObject, "CreatureHealthBarCanvas").GetComponent<RectTransform>();
        mainCameraTransform = GameObject.Find("Main Camera").transform;
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();

        // Start moving towards the player if he is in a certain range
        // Make sure that the max distance specified here ist the opposite of the distance specified in the Move method of Character!!!
        if (Vector3.Distance(transform.position, player.transform.position) < 10.0f && Vector3.Distance(transform.position, player.transform.position) > 1.0f)
        {
            Move(player.transform.position, Quaternion.LookRotation(player.transform.position - transform.position).normalized, 1.0f);
        }
        else
        {
            anim.SetBool("isMoving", false);
        }
        
        if (Vector3.Distance(transform.position, player.transform.position) <= 1.0f && player.enabled)
        {
            anim.SetBool("isInRange", true);
            if (Time.time >= nextAttack)
            {
                Attack(player);
                nextAttack = Time.time + anim.GetCurrentAnimatorStateInfo(0).length;
            }
        }
        else
        {
            anim.SetBool("isInRange", false);
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

        healthBarCanvas.rotation = mainCameraTransform.rotation;
    }
}
