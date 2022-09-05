using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


//[RequireComponent(typeof(Attack))]
public class Ronin : Enemy
{
    [SerializeField] Navigation navigate;
    [SerializeField] Attack attack;
    [SerializeField] private float distanceToAttack;
    [SerializeField] private float timeBetweenAtack;
    float curentAttackTime;



    // Start is called before the first frame update
    void Start()
    {
        navigate = GetComponent<Navigation>();
        attack = GetComponent<Attack>();
        if (attack == null)
        {
            Debug.Log("No attack type found");
        }
        anim = GetComponent<Animator>();
        setupAgent();
        StartCoroutine(GetPlayer());
    }

    private void setupAgent()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.stoppingDistance = distanceToAttack;
        agent.speed = speed;
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (health > 0)
        {
            if (!flinch)
            {
                if (!attacking){
                    moving = navigate.Navigating(target, distanceToAttack);
                }
                
                anim.SetBool("Moving", moving);

                curentAttackTime += Time.deltaTime;
                if (!attacking && !moving)
                {
                    anim.SetBool("Reached", true);
                    if (curentAttackTime > timeBetweenAtack)
                    {
                        Attack();
                    }
                }
                else if (moving)
                {
                    if (!attacking)
                    {
                        agent.speed = speed;
                        anim.SetBool("Reached", false);
                    }
                }
            }


            if (damaged && Time.time - lastDamageTime >= flinchTime)
            {
                Hurt(takenDamage);
            }
        }
    }

    private void Attack()
    {
        agent.speed = 0;
        curentAttackTime = 0f;
        attacking = true;
        targetAttackPoint = target.position;
        if (attack != null)
        {
            anim.SetTrigger("Attack");
        }
    }

    public void RecordAttack()
    {
        attack.Attacking(targetAttackPoint);
    }

    public void FinishAttack()
    {
        agent.speed = speed;
        attacking = false;
        //anim.Play("CombatIdle");
    }
}
