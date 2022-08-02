using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    //state machine
    private Vector3 startingPosition;

    [SerializeField] protected Transform target;
    public float flinchTime;
    
    public int health;
    public float speed;
    public int damage;
    public float distanceToAttack;
    protected bool flinch;
    
    protected NavMeshAgent agent;
    // Start is called before the first frame update
    void Start()
    {
        setupAgent();
    }

    
    protected void setupAgent(){
        startingPosition = transform.position;
        target = GameObject.FindGameObjectWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        agent.stoppingDistance = distanceToAttack;
        agent.speed = speed;
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    // Update is called once per frame
    void Update()
    {
        agent.SetDestination(target.position);        
    }
    virtual public void TakeDamage(int damage){
        flinch = true;
        agent.speed = 0;
        StartCoroutine(Hurt(damage));       
    }

    protected IEnumerator Hurt(int damage)
    {             
        yield return new WaitForSeconds(flinchTime);
        health -= damage;
        if(health <=0){
            Death();
        }else {
            agent.speed = speed;
            flinch = false;
        }  
        
    }
    private void Death(){
        agent.speed = 0;
        Debug.Log("died");
    }

    public bool isDead(){
        if (health <= 0){
            return true;
        }else{
            return false;
        }
        
    }
}
