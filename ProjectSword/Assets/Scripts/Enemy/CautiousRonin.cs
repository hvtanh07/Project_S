using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CautiousRonin : Enemy
{
    [SerializeField] Attack attack;
    [SerializeField] private float distanceToAttack;
    [SerializeField] private float timeBetweenAtack;
    float curentAttackTime;
    bool backingUp;
    Vector3 finaltarget;

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
                    var dir = target.position - transform.position;
                    var distance = dir.magnitude;
                    finaltarget = target.position;
                    if (distance < distanceToAttack - 1){
                        backingUp = true;
                        agent.stoppingDistance = 0;
                        finaltarget = new Vector3(
                            target.position.x + distanceToAttack * Mathf.Cos(Vector3.Angle(transform.position,target.position)*Mathf.Deg2Rad),
                            target.position.y + distanceToAttack * Mathf.Sin(Vector3.Angle(transform.position,target.position)*Mathf.Deg2Rad),
                            target.position.z);   
                    }
                    else if (distance >= distanceToAttack - 1){
                        backingUp = false;
                        agent.stoppingDistance = distanceToAttack;
                    }
                    
                    agent.destination = finaltarget;
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
                if (agent.remainingDistance <= distanceToAttack && !agent.pathPending && !stopping && !backingUp)
                { 
                    anim.SetBool("Reached", true);
                    if(curentAttackTime > timeBetweenAtack){
                        Attack();
                    }      
                }
                else if(agent.remainingDistance > distanceToAttack || backingUp)
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
        agent.speed = 0;
        curentAttackTime = 0f;
        stopping = true;
        targetAttackPoint = target.position;
        if(attack != null){
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
    private void OnDrawGizmos() {
        Gizmos.DrawSphere(finaltarget,0.5f);
    }
}
