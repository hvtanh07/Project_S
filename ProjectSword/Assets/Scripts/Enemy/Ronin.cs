using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


//[RequireComponent(typeof(Attack))]
public class Ronin : Enemy
{
    [SerializeField] Attack attack;
    private NavMeshAgent agent;
    public float flinchTime;
    public float distanceToAttack;
    private bool flinch;  
    public float timeBetweenAtack;
    float curentAttactTime;

    // Start is called before the first frame update
    void Start()
    {
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
        if(target != null){
            agent.SetDestination(target.position);
        } 
        curentAttactTime += Time.deltaTime;
        if(health > 0){
            if (agent.remainingDistance <= agent.stoppingDistance && curentAttactTime > timeBetweenAtack){                
                curentAttactTime = 0f;        
                attack.Attacking(target.position);
            }
        }
        
    }

    public override void TakeDamage(int damage){
        flinch = true;
        agent.speed = 0;
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
            agent.speed = speed;
            flinch = false;
        }  
        
    }
    
    override protected void Death(){
        agent.speed = 0;
        base.Death();
    }
}
