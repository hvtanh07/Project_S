using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class TouchAttack : Attack
{
    Player player;
    Collider2D col;
    public int damage;

    bool touchingPlayer;
    private void Start() {
        
        col = GetComponent<Collider2D>();
    }
    private void OnTriggerEnter2D(Collider2D other) {       
        player = other.GetComponent<Player>();
        if(player != null){
            touchingPlayer = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other) {
        player = other.GetComponent<Player>();
        if(player != null){
            touchingPlayer = false;
        }
    }
    public override void Attacking(Vector3 target){
        if(touchingPlayer){
            player.TakeDamage(damage);
        }
    }
}
