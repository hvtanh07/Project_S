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
    float curentAttactTime;
    bool chasing;
    

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
        if(health > 0){
            if(target != null && chasing){
                agent.SetDestination(target.position);
                anim.Play("Run");
            } 
            curentAttactTime += Time.deltaTime;
            if (agent.remainingDistance <= agent.stoppingDistance && curentAttactTime > timeBetweenAtack){  
                anim.Play("CombatIdle");
                chasing = false;          
                curentAttactTime = 0f;
                targetAttackPoint = target.position;      
                StartCoroutine(triggerAttack());    
            } 
        }    
    }
    IEnumerator triggerAttack(){ 
        yield return new WaitForSeconds (timeBeforeAttack);
        if(health > 0){
            anim.Play("Attack");         
        }
    }

    public void RecordAttack(){
        attack.Attacking(targetAttackPoint);
    }

    public void FinishAttack(){
        chasing = true;
        anim.Play("CombatIdle");
    }

    public override void TakeDamage(int damage){
        chasing = false;
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

    
}
