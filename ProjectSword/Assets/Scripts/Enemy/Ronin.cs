using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


//[RequireComponent(typeof(Attack))]
public class Ronin : Enemy
{
    [SerializeField] Attack attack;
    Animator anim;
    private NavMeshAgent agent;
    public float flinchTime;
    public float distanceToAttack;
    private bool flinch;  
    public float timeBetweenAtack;
    public float timeBeforeAttack;
    [SerializeField] float curentAttactTime;
    bool chasing;
    bool attacking;
    private bool m_FacingRight = false;
    

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        setupAgent();
        StartCoroutine(GetPlayer());
    }

    private void setupAgent(){
        agent = GetComponent<NavMeshAgent>();
        agent.stoppingDistance = distanceToAttack;
        agent.speed = speed;
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    protected IEnumerator GetPlayer()
    { 
        yield return new WaitForSeconds(1.0f);
        target = GameObject.FindGameObjectWithTag("Player").transform;
        chasing = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(health > 0 && !flinch){
            if(target != null && chasing){
                agent.SetDestination(target.position);
                if (agent.velocity.x > 0 && !m_FacingRight)
		        {
			        Flip();
		        }
		        else if (agent.velocity.x < 0 && m_FacingRight)
		        {
			        Flip();
		        }
            } 
            curentAttactTime += Time.deltaTime;
            if (agent.remainingDistance <= agent.stoppingDistance && !attacking){ 
                anim.Play("CombatIdle");
                chasing = false;
                if(curentAttactTime > timeBetweenAtack){
                    Debug.Log("attack");
                    Debug.ClearDeveloperConsole();
                    curentAttactTime = 0f;
                    targetAttackPoint = target.position;   
                    StartCoroutine(triggerAttack());
                }   
            }else{
                //Debug.Log("no longer at player pos");
                chasing = true;
                anim.Play("Run");
            }
        }    
    }
    IEnumerator triggerAttack(){ 
        yield return new WaitForSeconds (timeBeforeAttack);
        if(health > 0 && !flinch){
            attacking = true;
            anim.Play("Attack");         
        }
    }

    public void RecordAttack(){
        attack.Attacking(targetAttackPoint);
    }

    public void FinishAttack(){
        attacking = false;
        chasing = true;
        //anim.Play("CombatIdle");
    }

    public override void TakeDamage(int damage){
        chasing = false;
        flinch = true;
        agent.speed = 0;
        anim.Play("Idle");
        StartCoroutine(Hurt(damage));       
    }

    protected IEnumerator Hurt(int damage)
    {             
        yield return new WaitForSeconds(flinchTime);
        Shield shield = GetComponent<Shield>();
        if (shield != null){
            health -= shield.Block(damage);
        }else{
            health -= damage;
        }
        //------------------------
        if(health <= 0){
            Death();
        }else {
            anim.Play("Hurt");
            agent.speed = speed;
            chasing = true;
            flinch = false;
        }  
    }
    
    override protected void Death(){
        anim.Play("Death");
        agent.speed = 0;
        base.Death();
    }

    private void Flip()
	{
		// Switch the way the player is labelled as facing.
		m_FacingRight = !m_FacingRight;

		// Multiply the player's x local scale by -1.
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
}
