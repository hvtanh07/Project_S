using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blocker : Enemy
{
    [Header("Blocker stats")]
    public int maximumBlockHealth;
    public int blockingHealth;
    public int healAmount;
    public float blockHealTime = 1;
    private float lastBlockTime = -1;
    float attackRate = 1.0f; 
    float currentDamageTime;
    // Start is called before the first frame update
    void Start()
    {
        setupAgent();
        blockingHealth = maximumBlockHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if(health > 0){
            agent.SetDestination(target.position);
        }
        if ( blockingHealth < maximumBlockHealth && Time.time - lastBlockTime >= blockHealTime && health > 0)
        {
            blockingHealth += healAmount;
            lastBlockTime = Time.time;
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
    override public void TakeDamage(int damage){
        flinch = true;
        agent.speed = 0;
        if (blockingHealth <=0){
            StartCoroutine(Hurt(damage));  
        }else{
            if(health > 0){
                blockingHealth -= damage;
                lastBlockTime = Time.time;
                StartCoroutine(Flinch());
                //if (animator.GetCurrentAnimatorStateInfo(0).IsName("YourAnimationName"))
                //{
                    // agent.speed = speed;
                //}
            }
            
        }          
    }

    protected IEnumerator Flinch()//remove when have animation
    {             
        yield return new WaitForSeconds(1.0f);
        agent.speed = speed;
    }
}
