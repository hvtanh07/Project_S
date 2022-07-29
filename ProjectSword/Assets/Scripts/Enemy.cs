using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] Transform target;
    public float flinchTime;
    NavMeshAgent agent;
    public int health;
    public float speed;
    public int damage;
    float attackRate = 1.0f; 
    [SerializeField]float currentDamageTime;
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
    private void OnTriggerEnter2D(Collider2D other) {       
        Player player = other.GetComponent<Player>();
        if(player != null && !flinch){
            Debug.Log(other.gameObject.name);
            player.TakeDamage(damage);
            currentDamageTime = 0.0f;
        }
    }
    private void OnTriggerStay2D(Collider2D other) {
        Debug.Log("staying");
        Player player = other.GetComponent<Player>();
        if(player != null && !flinch){
            currentDamageTime += Time.deltaTime;
            if(currentDamageTime > attackRate)
            {
                player.TakeDamage(damage);
                currentDamageTime = 0.0f;
            }
        }
    }

}
