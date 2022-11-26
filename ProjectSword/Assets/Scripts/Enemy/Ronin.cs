using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


//[RequireComponent(typeof(Attack))]
public class Ronin : Enemy
{
    [SerializeField] Navigation navigate;
    [SerializeField] Attack attack;
    float curentAttackTime;



    // Start is called before the first frame update
    public void Setup()
    {
        navigate = GetComponent<Navigation>();
        attack = GetComponent<Attack>();
        if (attack == null)
        {
            Debug.Log("No attack type found");
        }
        animR = GetComponent<Animator>();
        setupAgent();
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void setupAgent()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.stoppingDistance = attack.distanceToAttack;
        agent.speed = speed;
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0) return;

        if (!flinch)
        {
            if (!attacking)
            {
                moving = navigate.Navigating(target, attack.distanceToAttack);
            }

            animR.SetBool("Moving", moving);

            curentAttackTime += Time.deltaTime;
            if (!attacking && !moving)
            {
                animR.SetBool("Reached", true);
                if (curentAttackTime > attack.timeBetweenAtack)
                {
                    Attack();
                }
            }
            else if (moving)
            {
                if (!attacking)
                {
                    agent.speed = speed;
                    animR.SetBool("Reached", false);
                }
            }
        }


        if (damaged && Time.time - lastDamageTime >= flinchTime)
        {
            Hurt(takenDamage);
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
            animR.SetTrigger("Attack");
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
