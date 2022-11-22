using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;

public class Enemy : MonoBehaviour
{
    [HideInInspector] protected Transform target;
    [HideInInspector] protected Vector3 targetAttackPoint;
    public Animator animR;
    [SerializeField] protected NavMeshAgent agent;
    public int health;
    public float speed;
    [SerializeField] protected float flinchTime;
    protected bool flinch;
    protected bool damaged;
    protected float lastDamageTime;
    protected int takenDamage;
    protected bool attacking;
    protected bool moving;
    protected bool m_FacingRight = false;

    private void FixedUpdate()
    {
        if (health > 0)
        {
            if (agent.remainingDistance <= agent.stoppingDistance + 3)
            {
                agent.radius = Mathf.MoveTowards(agent.radius, 0.01f, 0.01f);
                agent.height = Mathf.MoveTowards(agent.height, 0.01f, 0.01f);
            }
            else
            {
                agent.radius = Mathf.MoveTowards(agent.radius, 0.5f, 0.01f);
                agent.height = Mathf.MoveTowards(agent.height, 1f, 0.02f);
            }
        }

    }
    public void TakeDamage(int damage)
    {
        if (health > 0)
        {
            Shield shield = GetComponent<Shield>();
            if (shield != null)
            {
                //Play anim Block
                health -= shield.Block(damage);
            }
            else
            {
                flinch = true;
                agent.speed = 0;
                animR.SetBool("Moving", false);
                animR.Play("Idle");
                damaged = true;
                agent.radius = 0;
                agent.height = 0;
                takenDamage += damage;
                lastDamageTime = Time.time;
            }
        }
    }


    public void InstantTakeDamage(int damage)
    {
        if (health > 0)
        {
            agent.SetDestination(gameObject.transform.position);
            Shield shield = GetComponent<Shield>();
            if (shield != null)
            {
                //Play Anim Block
                health -= shield.Block(damage);
            }
            else
            {
                flinch = true;
                agent.speed = 0;
                animR.SetBool("Moving", false);
                agent.radius = 0;
                agent.height = 0;
                lastDamageTime = Time.time;
                Hurt(damage);
            }

        }
    }

    public void TakeDamageFinished (){
        agent.speed = speed;
        flinch = false;
    }


    protected void Hurt(int damage)
    {
        damaged = false;
        health -= damage;
        animR.SetTrigger("Hurt");
        takenDamage = 0;
        //------------------------
        if (health <= 0)
        {
            Death();
        }
    }

    private void Death()
    {
        animR.SetBool("Die", true);
        BattleSystem.instance.enemyKilled();
        GetComponent<BoxCollider2D>().enabled = false;
        Destroy(gameObject, 3.0f);
    }

    public void Spawn()
    {
        gameObject.SetActive(true);
    }


}
