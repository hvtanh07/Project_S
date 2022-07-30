using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailControl : MonoBehaviour
{
    int slashDamage;
    private void Start() {
        GameObject player = GameObject.FindWithTag("Player");
        slashDamage = player.GetComponent<Player>().damage;
    }
    private void OnTriggerEnter2D(Collider2D other){        
        Enemy enemy = other.GetComponent<Enemy>();
        if(enemy != null){
            enemy.TakeDamage(slashDamage);
            return;
        }
        DashEnemy enemy1 = other.GetComponent<DashEnemy>();
        if(enemy1 != null){
            enemy1.TakeDamage(slashDamage);
            return;
        }

    }
}
