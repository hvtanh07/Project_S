using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShockWave : SpecialAttack
{
    public float radius;
    public float force;
    public int Maximumdamage;
    public override void Attack(){
        //Instantiate()

        Collider2D[] collidersToAttack = Physics2D.OverlapCircleAll(new Vector2(transform.position.x,transform.position.y),radius);

        foreach(Collider2D nearByEnemy in collidersToAttack){
            Enemy enemy = nearByEnemy.GetComponent<Enemy>();
            if(enemy != null){
                enemy.InstantTakeDamage(Maximumdamage);
            }
        }

        Collider2D[] collidersToPush = Physics2D.OverlapCircleAll(new Vector2(transform.position.x,transform.position.y),radius);

        foreach(Collider2D nearByObject in collidersToPush){
            Rigidbody2D obj = nearByObject.GetComponent<Rigidbody2D>();
            if(obj != null){
                var explosionDir = obj.position - new Vector2(transform.position.x,transform.position.y);
                var explosionDistance = explosionDir.magnitude;
                explosionDir /= explosionDistance;

                obj.AddForce(Mathf.Lerp(0, force, (1 - explosionDistance)) * explosionDir);
                //obj.AddForce(force, transform)
            }
        }
    }
}
