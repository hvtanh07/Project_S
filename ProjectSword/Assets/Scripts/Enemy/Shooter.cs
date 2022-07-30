using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : Enemy
{
    [SerializeField] GameObject objBullet;
    public float bulletSpeed;
    public float distanceToShoot;
    public float timeBeforeShoot;
    public float fireRate;
    float curentFireTime;
    // Start is called before the first frame update
    void Start()
    {
        setupAgent();
        agent.stoppingDistance = distanceToShoot;
    }

    // Update is called once per frame
    void Update()
    {
        agent.SetDestination(target.position);
        curentFireTime += Time.deltaTime;
        if (agent.remainingDistance <= agent.stoppingDistance && curentFireTime > fireRate && health > 0){
            curentFireTime = 0f;      
            agent.speed = 0;            
            StartCoroutine(Shoot(target.position));
        }else if(agent.remainingDistance > agent.stoppingDistance){
            agent.speed = speed; 
        }
    }

    IEnumerator Shoot(Vector3 target){
        yield return new WaitForSeconds(timeBeforeShoot); 
          
        Quaternion rotation = Quaternion.LookRotation(transform.forward, target - transform.position); 
        GameObject bullet = Instantiate(objBullet,transform.position,rotation);
        
        //LeanTween.move(bullet,target*10,)
    }
}
