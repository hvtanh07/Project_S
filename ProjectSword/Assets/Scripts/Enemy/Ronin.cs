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
    }

    // Update is called once per frame
    void Update()
    {
        if(health > 0 && !flinch){
            if(target != null){
                agent.SetDestination(target.position);
                anim.SetBool("Moving", true);
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
            if (agent.remainingDistance <= agent.stoppingDistance){ 
                anim.SetBool("Reached", true);
                if(curentAttactTime > timeBetweenAtack){
                    Debug.ClearDeveloperConsole();
                    curentAttactTime = 0f;
                    targetAttackPoint = target.position;   
                    StartCoroutine(triggerAttack());
                } 
            }else{
                //Debug.Log("no longer at player pos");
                anim.SetBool("Reached", false);
            }
        }    
    }
    IEnumerator triggerAttack(){ 
        yield return new WaitForSeconds (timeBeforeAttack);
        if(health > 0 && !flinch){
            anim.SetTrigger("Attack");   
        }
    }

    public void RecordAttack(){
        attack.Attacking(targetAttackPoint);
    }

    public void FinishAttack(){
        //anim.Play("CombatIdle");
    }

    public override void TakeDamage(int damage){
        flinch = true;
        agent.speed = 0;
        anim.SetBool("Moving", false);
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
            anim.SetTrigger("Hurt");
        }
        //------------------------
        if(health <= 0){
            Death();
        }else {
            agent.speed = speed;
            flinch = false;
        }  
    }
    
    override protected void Death(){
        anim.SetBool("Die", true);
        GetComponent<BoxCollider2D>().enabled = false;
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
