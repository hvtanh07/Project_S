using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Socerer : Enemy
{
    public enum MagicType{
        circle,
        random,
        line
    }
    [Header("Shooter stats")]
    [SerializeField] GameObject objBullet;
    public float timeBeforeShoot;
    public float fireRate;
    public float spotWidth;
    public float spotSpreadRange;
    public int numberOfAttackPoint;   
    public MagicType typeOfMagic;
    public LayerMask playerMask;
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
            switch(typeOfMagic){
                case MagicType.circle:{
                    StartCoroutine(CircleSpot(target.position));
                    break;
                }
                    
                case MagicType.random:{
                    StartCoroutine(RandomSpot(target.position));
                    break;
                }
                    
                case MagicType.line:{
                    StartCoroutine(StraightLineSpot(target.position));
                    break;
                }       
            }        
        }else if(agent.remainingDistance > agent.stoppingDistance){
            agent.speed = speed; 
        }
    }
    IEnumerator RandomSpot(Vector3 target){
        yield return new WaitForSeconds(timeBeforeShoot); 
        if (health > 0){
            float minXRange = target.x - spotSpreadRange;
            float maxXRange = target.x + spotSpreadRange;
            float minYRange = target.y - spotSpreadRange;
            float maxYRange = target.y + spotSpreadRange;
                       
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
                Debug.DrawLine(spot,spot+(Vector2.up * spotWidth),Color.red,1.0f);
                Debug.DrawLine(spot,spot+(Vector2.down * spotWidth),Color.yellow,1.0f);
                Debug.DrawLine(spot,spot+(Vector2.right * spotWidth),Color.black,1.0f);
                Debug.DrawLine(spot,spot+(Vector2.left * spotWidth),Color.blue,1.0f);          
            }
        }
    }   
    IEnumerator CircleSpot(Vector3 target){
        yield return new WaitForSeconds(timeBeforeShoot); 
        if (health > 0){
            float angle = 360.0f / numberOfAttackPoint;
            List<Vector3> spotList = new List<Vector3>();
            for (int i =0; i< numberOfAttackPoint; i++){             
                Vector3 offset = Quaternion.Euler(0, 0, angle * i) * Vector3.right*spotSpreadRange;
                Vector3 spot = target + offset;
                spotList.Add(spot);
            }
            foreach(Vector2 spot in spotList){
                Collider2D hitEnemies = Physics2D.OverlapCircle(spot, spotWidth, playerMask);
                if (hitEnemies != null){
                    hitEnemies.GetComponent<Player>().TakeDamage(damage); 
                }   
                Debug.DrawLine(spot,spot+(Vector2.up * spotWidth),Color.red,1.0f);
                Debug.DrawLine(spot,spot+(Vector2.down * spotWidth),Color.yellow,1.0f);
                Debug.DrawLine(spot,spot+(Vector2.right * spotWidth),Color.black,1.0f);
                Debug.DrawLine(spot,spot+(Vector2.left * spotWidth),Color.blue,1.0f);          
            }
        }
    }
    IEnumerator StraightLineSpot(Vector3 target){
        yield return new WaitForSeconds(timeBeforeShoot); 
        if (health > 0){
            int halfLine = numberOfAttackPoint/2;
            Vector3 dir = (target - transform.position).normalized;
            List<Vector3> spotList = new List<Vector3>();
            for (int i = -halfLine; i <= halfLine; i++){                          
                Vector3 spot = target + dir*i;
                spotList.Add(spot);
            }
            foreach(Vector2 spot in spotList){
                Collider2D hitEnemies = Physics2D.OverlapCircle(spot, spotWidth, playerMask);
                if (hitEnemies != null){
                    hitEnemies.GetComponent<Player>().TakeDamage(damage); 
                }   
                Debug.DrawLine(spot,spot+(Vector2.up * spotWidth),Color.red,1.0f);
                Debug.DrawLine(spot,spot+(Vector2.down * spotWidth),Color.yellow,1.0f);
                Debug.DrawLine(spot,spot+(Vector2.right * spotWidth),Color.black,1.0f);
                Debug.DrawLine(spot,spot+(Vector2.left * spotWidth),Color.blue,1.0f);          
            }
        }
    }
}