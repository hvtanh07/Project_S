using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


//[RequireComponent(typeof(Attack))]
public class Ronin : Enemy
{
    [SerializeField] Attack attack;
    [SerializeField] private float distanceToAttack;
    [SerializeField] private float timeBetweenAtack;
    [SerializeField] private float timeBeforeAttack;
    float curentAttackTime;

    

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
                curentAttackTime += Time.deltaTime;
                if (agent.remainingDistance <= agent.stoppingDistance && !agent.pathPending && !stopping)
                { 
                    anim.SetBool("Reached", true);
                    if(curentAttackTime > timeBetweenAtack){
                        curentAttackTime = 0f;
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
}
