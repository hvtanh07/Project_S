using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kunai : MonoBehaviour
{
    private void Start() {
        Destroy(gameObject,5.0f);
    }
    public int damage;
    private void OnTriggerEnter2D(Collider2D other) {
        Enemy enemy = other.gameObject.GetComponent<Enemy>();
        if(enemy != null){
            enemy.InstantTakeDamage(damage);    
        }
        Destroy(gameObject,0.05f); 
    }
    
}
