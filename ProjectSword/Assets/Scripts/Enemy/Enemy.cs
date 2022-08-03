using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;

public class Enemy : MonoBehaviour
{
    //state machine
    private Vector3 startingPosition;
    public event EventHandler OnEnemyDie;

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
        agent.speed = 0;
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
        OnEnemyDie?.Invoke(this, EventArgs.Empty);
        GetComponent<BoxCollider2D>().enabled = false;
        Destroy(gameObject, 3.0f);
    }


    public void Spawn(){
        gameObject.SetActive(true);
        //agent.speed = 0;
        StartCoroutine(StartMoving());
    }

    protected IEnumerator StartMoving()
    {             
        yield return new WaitForSeconds(0.5f);
        agent.speed = speed;   
    }
}
