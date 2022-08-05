using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    Rigidbody2D rb;
    public float flySpeed;
    private void Start() {
        Destroy(gameObject,5.0f);
    }
    public int damage;
    private void OnTriggerEnter2D(Collider2D other) {
        Player player = other.gameObject.GetComponent<Player>();
        if(player != null){
            player.TakeDamage(damage);    
        }
        Destroy(gameObject,0.05f);
    }
    
}
