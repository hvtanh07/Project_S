using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Shogun : Enemy
{
    [SerializeField] Navigation navigate;
    [SerializeField] Attack rangeAttack;
    [SerializeField] Attack closeAttack;
    Attack currentAttack;
    public float rangeAttackRange; 
    public float closeAttackRange; 
    [SerializeField] private float timeBetweenAtack;
    
    float curentAttackTime;
    // Start is called before the first frame update
    void Start()
    {
        navigate = GetComponent<Navigation>();
        anim = GetComponent<Animator>();
        setupAgent();
        //StartCoroutine(GetPlayer());
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
                moving = navigate.Navigating(target,rangeAttackRange);
                Debug.Log(moving);

                anim.SetBool("Moving",moving);

                curentAttackTime += Time.deltaTime;
                if (!attacking && !moving)
                {    
                    currentAttack = rangeAttack;
                    if (agent.remainingDistance <= closeAttackRange)     
                        currentAttack = closeAttack;       
                    anim.SetBool("Reached", true);
                    if(curentAttackTime > timeBetweenAtack){
                        Attack();
                    }    
                }else if(moving)
                {
                    if(!attacking){
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
        attacking = true;
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
        attacking = false;
        //anim.Play("CombatIdle");
    }
    
}
