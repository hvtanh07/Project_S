using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Socerer : Enemy
{
    [Header("Shooter stats")]
    [SerializeField] GameObject objBullet;
    public float distanceToShoot;
    public float timeBeforeShoot;
    public float fireRate;
    public float spotWidth;
    public float spotSpreadRange;
    public int numberOfAttackPoint;   
    public LayerMask playerMask;
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
            float minXRange = target.x - spotSpreadRange;
            float maxXRange = target.x + spotSpreadRange;
            float minYRange = target.x - spotSpreadRange;
            float maxYRange = target.x + spotSpreadRange;
                       
            List<Vector2> randomSpot = new List<Vector2>();
            for (int i =0; i< numberOfAttackPoint; i++){
                Vector2 Spot = new Vector2(Random.Range(minXRange,maxXRange),Random.Range(minYRange,maxYRange));
                randomSpot.Add(Spot);
            }
            foreach(Vector2 spot in randomSpot){
                Collider2D hitEnemies = Physics2D.OverlapCircle(spot, spotWidth, playerMask);
                if (hitEnemies != null){
                    hitEnemies.GetComponent<Player>().TakeDamage(damage); 
                }   
                //Debug.DrawLine(spot,spot+(Vector2.up * spotWidth),Color.red,1.0f);
                //Debug.DrawLine(spot,spot+(Vector2.down * spotWidth),Color.yellow,1.0f);
                //Debug.DrawLine(spot,spot+(Vector2.right * spotWidth),Color.black,1.0f);
                //Debug.DrawLine(spot,spot+(Vector2.left * spotWidth),Color.blue,1.0f);          
            }
        }
    }   
}