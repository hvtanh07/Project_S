using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShockWave : SpecialAttack
{
    public float radius;
    public int maximumDamage;
    public LayerMask effectMask;
    public override void Attack(){
        //Instantiate()

        Collider2D[] collidersToAttack = Physics2D.OverlapCircleAll(new Vector2(transform.position.x,transform.position.y),radius);

        foreach(Collider2D nearByEnemy in collidersToAttack){
            Enemy enemy = nearByEnemy.GetComponent<Enemy>();
            if(enemy != null){
                var explosionDistance = (nearByEnemy.transform.position - transform.position).magnitude;
                float effectiveRange;
                if(explosionDistance > 1 ){
                    effectiveRange  = 1 / explosionDistance;
                }  
                else{
                    effectiveRange  = 1;
                }

                enemy.InstantTakeDamage(Mathf.RoundToInt(effectiveRange * maximumDamage));
            }
        }
    }
}
