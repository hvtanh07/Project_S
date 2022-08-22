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
    [SerializeField] private float flinchTime;
    [SerializeField] private float distanceToAttack;
    private bool flinch;  
    [SerializeField] private float timeBetweenAtack;
    [SerializeField] private float timeBeforeAttack;
    private bool damaged;
    private float lastDamageTime;
    private int takenDamage;
    private bool stopping;
    float curentAttactTime;
    private bool m_FacingRight = false;
    

    // Start is called before the first frame update
    void Start()
    {
        attack = GetComponent<Attack>();
        if(attack == null){
            Debug.Log("No attack type found");
        }
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
        if(health > 0){
            if(!flinch)
            {
                if(target != null){
                    agent.destination = target.position;
                    //agent.SetDestination(target.position);
                
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
                if (agent.remainingDistance <= agent.stoppingDistance && !agent.pathPending && !stopping)
                { 
                    anim.SetBool("Reached", true);
                    if(curentAttactTime > timeBetweenAtack){
                        curentAttactTime = 0f;
                        stopping = true;
                        targetAttackPoint = target.position;
                        StartCoroutine(triggerAttack());
                    }      
                }
                else
                {
                    //Debug.Log("no longer at player pos");
                    //stopping = false;
                    if(!stopping){
                        agent.speed = speed;
                        anim.SetBool("Reached", false);
                    }
                    
                }
            }  

            if ( damaged && Time.time - lastDamageTime >= flinchTime )
            {
                Hurt(takenDamage);
            }
        }    
    }
    private void FixedUpdate() {
        if(agent.remainingDistance <= agent.stoppingDistance + 3 || health <= 0){
                agent.radius = Mathf.MoveTowards(agent.radius,0.01f,0.01f);
                agent.height = Mathf.MoveTowards(agent.height,0.01f,0.01f);
                //agent.radius = 0.01f;
            }else{
                agent.radius = Mathf.MoveTowards(agent.radius,0.5f,0.01f);
                agent.height = Mathf.MoveTowards(agent.height,1f,0.02f);
                //agent.radius = 0.5f;
            }
    }
    IEnumerator triggerAttack(){ 
        agent.speed = 0;
        yield return new WaitForSeconds (timeBeforeAttack);
        if(health > 0 && !flinch && attack != null){
            anim.SetTrigger("Attack");   
        }
    }

    public void RecordAttack(){
        attack.Attacking(targetAttackPoint);
    }

    public void FinishAttack(){
        agent.speed = speed;
        stopping = false;
        //anim.Play("CombatIdle");
    }

    public override void TakeDamage(int damage){
        if(health > 0){
            flinch = true;
            agent.speed = 0;
            anim.SetBool("Moving", false);
            anim.Play("Idle");
            damaged = true;
            takenDamage += damage;
            lastDamageTime = Time.time;
            //StartCoroutine(Hurt(damage));  
        }        
    }

    private void Hurt(int damage)
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
    
    override protected void Death(){
        anim.SetBool("Die", true);
        GetComponent<BoxCollider2D>().enabled = false;
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
