using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : Enemy
{
    [Header("Shooter stats")]
    [SerializeField] GameObject objBullet;
    public float timeBeforeShoot;
    public float fireRate;
    float curentFireTime;
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
        
        curentFireTime += Time.deltaTime;
        if (agent.remainingDistance <= agent.stoppingDistance && curentFireTime > fireRate){
            curentFireTime = 0f;      
            agent.speed = 0;            
            StartCoroutine(Shoot(target.position));
        }else if(agent.remainingDistance > agent.stoppingDistance){
            agent.speed = speed; 
        }
    }

    IEnumerator Shoot(Vector3 target){
        yield return new WaitForSeconds(timeBeforeShoot); 
        if (health > 0){
            Quaternion rotation = Quaternion.LookRotation(transform.forward, target - transform.position); 
            GameObject bullet = Instantiate(objBullet,transform.position,rotation);      
        }
    }
}
