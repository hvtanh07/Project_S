using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] Transform target;
    NavMeshAgent agent;
    public int health;
    public float speed;
    public int damage;
    private bool flinch;
    // Start is called before the first frame update
    void Start()
    {
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
        agent.speed = 0;
        StartCoroutine(Hurt(0.5f, damage));       
    }

    IEnumerator Hurt(float secs, int damage)
    {      
        yield return new WaitForSeconds(secs);
        health -= damage;
        if(health <=0){
            Death();
        }else {
            agent.speed = speed;
        }  
        
    }
    private void Death(){
        Debug.Log("died");
    }
}
