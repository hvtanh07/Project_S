using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;

public class Enemy : MonoBehaviour
{
    //state machine
    private Vector3 startingPosition;
    [HideInInspector] protected Transform target;
    [HideInInspector] protected Vector3 targetAttackPoint;
    protected Animator anim;
    protected NavMeshAgent agent;
    public int health;
    public float speed;
    [SerializeField] protected float flinchTime;
    protected bool flinch; 
    protected bool damaged;
    protected float lastDamageTime;
    protected int takenDamage;
    protected bool stopping;
    protected bool m_FacingRight = false;

    //protected NavMeshAgent agent;
    // Start is called before the first frame update

    protected IEnumerator GetPlayer()
    { 
        yield return new WaitForSeconds(1.0f);
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }
   
    private void FixedUpdate() {
        if(health > 0){
            if(agent.remainingDistance <= agent.stoppingDistance + 3){
                agent.radius = Mathf.MoveTowards(agent.radius,0.01f,0.01f);
                agent.height = Mathf.MoveTowards(agent.height,0.01f,0.01f);    
            }else{
                agent.radius = Mathf.MoveTowards(agent.radius,0.5f,0.01f);
                agent.height = Mathf.MoveTowards(agent.height,1f,0.02f);        
            }
        }
        
    }
    public void TakeDamage(int damage){
        if(health > 0){
            flinch = true;
            agent.speed = 0;
            anim.SetBool("Moving", false);
            anim.Play("Idle");
            damaged = true;
            agent.radius = 0;
            agent.height = 0;
            takenDamage += damage;
            lastDamageTime = Time.time;
            //StartCoroutine(Hurt(damage));  
        }        
    }

    public void InstantTakeDamage(int damage){
        if(health > 0){
            flinch = true;
            agent.speed = 0;
            anim.SetBool("Moving", false);
            agent.radius = 0;
            agent.height = 0;
            lastDamageTime = Time.time;
            
            
            Hurt(damage);
        }        
    }


    protected void Hurt(int damage)
    {             
        //yield return new WaitForSeconds(flinchTime);
        damaged = false;
        Shield shield = GetComponent<Shield>();
        if (shield != null){
            health -= shield.Block(damage);
        }else{
            health -= damage;
            anim.SetTrigger("Hurt");
        }
        takenDamage = 0;
        //------------------------
        if(health <= 0){
            Death();
        }else {
            agent.speed = speed;
            flinch = false;
        }  
    }
    
    private void Death(){
        anim.SetBool("Die", true);
        BattleSystem.instance.enemyKilled();
        GetComponent<BoxCollider2D>().enabled = false;
        Destroy(gameObject, 3.0f);
    }

    protected void Flip()
	{
		// Switch the way the player is labelled as facing.
		m_FacingRight = !m_FacingRight;

		// Multiply the player's x local scale by -1.
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
    public void Spawn(){
        gameObject.SetActive(true);
    }
    
    
}
