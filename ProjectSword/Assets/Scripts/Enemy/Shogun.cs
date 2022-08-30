using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Shogun : Enemy
{
    [SerializeField] Attack rangeAttack;
    [SerializeField] Attack closeAttack;
    Attack currentAttack;
    public float rangeAttackRange; 
    public float closeAttackRange; 
    [SerializeField] private float timeBetweenAtack;
    [SerializeField] private float timeBeforeAttack;
    
    float curentAttackTime;
    // Start is called before the first frame update
    void Start()
    {
        
        anim = GetComponent<Animator>();
        setupAgent();
        StartCoroutine(GetPlayer());
    }
    private void setupAgent(){
        agent = GetComponent<NavMeshAgent>();
        agent.stoppingDistance = rangeAttackRange;
        agent.speed = speed;
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }
    // Update is called once per frame
    void Update()
    {
        if(health > 0){
            if(!flinch){
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
                curentAttackTime += Time.deltaTime;
                if (agent.remainingDistance <= agent.stoppingDistance && !agent.pathPending && !stopping)
                {    
                    currentAttack = rangeAttack;
                    if (agent.remainingDistance <= closeAttackRange)     
                        currentAttack = closeAttack;       
                    anim.SetBool("Reached", true);
                    if(curentAttackTime > timeBetweenAtack){
                        Attack();
                    }    
                }else if(agent.remainingDistance > agent.stoppingDistance)
                {
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

    private void Attack(){ 
        curentAttackTime = 0f;
        stopping = true;
        targetAttackPoint = target.position;
        agent.speed = 0;
        if(health > 0){
            anim.SetTrigger("Attack");   
        }
    }

    public void RecordAttack(){
        currentAttack.Attacking(targetAttackPoint);
    }

    public void FinishAttack(){
        agent.speed = speed;
        stopping = false;
        //anim.Play("CombatIdle");
    }
    
}
