using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chaser : Enemy
{
    float attackRate = 1.0f; 
    float currentDamageTime;
    // Start is called before the first frame update
    void Start()
    {
        setupAgent();
    }

    // Update is called once per frame
    void Update()
    {
        if(health > 0){
            agent.SetDestination(target.position);
        }
        
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
