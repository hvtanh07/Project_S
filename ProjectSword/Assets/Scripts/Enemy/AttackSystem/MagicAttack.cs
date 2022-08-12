using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicAttack : Attack
{
    private enum MagicType{
        circle,
        random,
        line
    }
    public GameObject lightning;
    public int damage;
    public float spotWidth;
    public float spotSpreadRange;
    public int numberOfAttackPoint;   
    [SerializeField] private MagicType typeOfMagic;
    public LayerMask playerMask;
    // Start is called before the first frame update
    public override void Attacking(Vector3 target){
        switch(typeOfMagic){
            case MagicType.circle:{
                CircleSpot(target);
                break;
            }
                    
            case MagicType.random:{
                RandomSpot(target);
                break;
            }
                    
            case MagicType.line:{
                StraightLineSpot(target);
                break;
            }              
        }    
    }

    private void RandomSpot(Vector3 target){
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
            GameObject lightningObject = Instantiate(lightning,spot,Quaternion.identity); 
            Destroy(lightningObject, 0.8f);
            if (hitEnemies != null){
                hitEnemies.GetComponent<Player>().TakeDamage(damage);    
            }      
        }    
    }   
    private void CircleSpot(Vector3 target){
        float angle = 360.0f / numberOfAttackPoint;
        List<Vector3> spotList = new List<Vector3>();
        for (int i =0; i< numberOfAttackPoint; i++){             
            Vector3 offset = Quaternion.Euler(0, 0, angle * i) * Vector3.right*spotSpreadRange;
            Vector3 spot = target + offset;
            spotList.Add(spot);
        }
        foreach(Vector2 spot in spotList){
            GameObject lightningObject = Instantiate(lightning,spot,Quaternion.identity); 
            Destroy(lightningObject, 0.8f);
            Collider2D hitEnemies = Physics2D.OverlapCircle(spot, spotWidth, playerMask);
            if (hitEnemies != null){
                hitEnemies.GetComponent<Player>().TakeDamage(damage); 
            }        
        }   
    }
    private void StraightLineSpot(Vector3 target){
        int halfLine = numberOfAttackPoint/2;
        Vector3 dir = (target - transform.position).normalized;
        List<Vector3> spotList = new List<Vector3>();
        for (int i = -halfLine; i <= halfLine; i++){                          
            Vector3 spot = target + dir*i;
            spotList.Add(spot);
        }
        foreach(Vector2 spot in spotList){
            GameObject lightningObject = Instantiate(lightning,spot,Quaternion.identity); 
            Destroy(lightningObject, 0.8f);
            Collider2D hitEnemies = Physics2D.OverlapCircle(spot, spotWidth, playerMask);
            if (hitEnemies != null){
                hitEnemies.GetComponent<Player>().TakeDamage(damage); 
            }   
        }    
    }
}
