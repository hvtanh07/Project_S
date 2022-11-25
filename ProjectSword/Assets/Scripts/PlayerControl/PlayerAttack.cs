using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public TrailRenderer trail;
    public SpecialAttack specialAttack;
    public int damage;
    Player player;
    public float dashDistance;
    public float dashSpeed;
    public float maxDashs;
    [SerializeField] private float numOfDashs;
    public float dashHealTime = 1;
    public LayerMask wallmask;
    public LayerMask enemies;
    private bool enemiesAround;
    private Animator anim;
    private float timer;
    private float lastDashTime = -1;
    // Start is called before the first frame update
    void Start()
    {
        numOfDashs = maxDashs;
        player = GetComponent<Player>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (numOfDashs < maxDashs && Time.time - lastDashTime >= dashHealTime)
        {
            addDash();
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            specialAttack.Attack(player.dir);
        }
    }

    private void FixedUpdate()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(new Vector2(transform.position.x, transform.position.y), dashDistance * 1.5f, enemies);
        if (colliders.Length > 0)
        {
            enemiesAround = true;
        }
        else
        {
            enemiesAround = false;
        }
    }

    public void Dash()
    {
        player.HoldingDown = false;
        anim.SetBool("Running", false);
        player.indicator.SetActive(false);
        if (numOfDashs > 0)
        {
            anim.SetTrigger("Attack");
            float animlength = anim.GetCurrentAnimatorStateInfo(0).length;
            //get trail and prepare to draw
            trail.transform.SetParent(this.transform);
            trail.transform.localPosition = Vector3.zero;
            trail.Clear();

            player.dir *= dashDistance;
            //set target
            Vector3 target = new Vector2(transform.position.x + player.dir.x, transform.position.y + player.dir.y);
            float dashLength = dashDistance;
            //draw line to front to check if dash will hit anything then dash to the target                   
            RaycastHit2D hit = Physics2D.Raycast(transform.position, player.dir, dashDistance, wallmask);
            if (hit.collider != null)
            {
                target = hit.point;
                dashLength = Vector3.Magnitude(transform.position - target);
            }
            player.dashing = true;
            player.walkable = false;
            anim.speed = (animlength * dashSpeed) / dashLength;
            LeanTween.move(this.gameObject, target, dashLength / dashSpeed).setEase(LeanTweenType.easeOutQuart).setOnComplete(FinishedDash);
        }
    }

    private void FinishedDash()
    {
        anim.Play("Idle");
        anim.speed = 1;
        trail.transform.SetParent(null);
        trail.Clear();
        player.dashing = false;
        player.walkable = true;
        numOfDashs--;
        lastDashTime = Time.time;
    }

    public void addDash()
    {
        numOfDashs++;
        lastDashTime = Time.time;
    }
}
