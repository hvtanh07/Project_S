using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] protected Transform target;
    public float flinchTime;
    
    public int health;
    public float speed;
    public int damage;
    protected bool flinch;
    protected NavMeshAgent agent;
    // Start is called before the first frame update
    void Start()
    {
        setupAgent();
    }
    protected void setupAgent(){
        target = GameObject.FindGameObjectWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        agent.speed = speed;
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    // Update is called once per frame
    void Update()
    {
        agent.SetDestination(target.position);        
    }
    public void TakeDamage(int damage){
        flinch = true;
        agent.speed = 0;
        StartCoroutine(Hurt(damage));       
    }

    IEnumerator Hurt(int damage)
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
        Debug.Log("died");
    }
}
