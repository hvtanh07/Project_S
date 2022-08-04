using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Shogun : Enemy
{
    [SerializeField] DashAttack dashAttack;
    [SerializeField] ProjectileAttack projectileAttack;
    private NavMeshAgent agent;
    public float damageReceiveDelay;
    public float rangedAttackRange; 
    public float dashAttackRange;
    public float attackRate;
    float curentAttactTime;
    // Start is called before the first frame update
    void Start()
    {
        setupAgent();
        StartCoroutine(GetPlayer());
    }
    private void setupAgent(){
        agent = GetComponent<NavMeshAgent>();
        agent.stoppingDistance = rangedAttackRange;
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
        if(target != null){
            agent.SetDestination(target.position);
        } 
        curentAttactTime += Time.deltaTime;
        if(health > 0){
            if (agent.remainingDistance <= rangedAttackRange && curentAttactTime > attackRate){                
                curentAttactTime = 0f;  
                if (agent.remainingDistance <= dashAttackRange){
                    dashAttack.Attacking(target.position);
                }else{
                    projectileAttack.Attacking(target.position);
                }
                //attack.Attacking(target.position);
            }
        }
    }

    public override void TakeDamage(int damage){
        StartCoroutine(Hurt(damage));       
    }

    protected IEnumerator Hurt(int damage)
    {             
        yield return new WaitForSeconds(damageReceiveDelay);
        Shield shield = GetComponent<Shield>();
        if (shield != null){
            health -= shield.Block(damage);
        }else{
            health -= damage;
        }
        //------------------------
        if(health <= 0){
            Death();
        }
    }
    
    override protected void Death(){
        agent.speed = 0;
        base.Death();
    }
}
