using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    Rigidbody2D rb;
    public float flySpeed;
    private void Start() {
        rb = GetComponent<Rigidbody2D>();
        Destroy(gameObject,5.0f);
    }
    private void FixedUpdate() {        
        transform.position += transform.TransformDirection(Vector3.up * flySpeed * Time.deltaTime);    
    }
    public int damage;
    private void OnTriggerEnter2D(Collider2D other) {
        Player player = other.gameObject.GetComponent<Player>();
        if(player != null){
            player.TakeDamage(damage);    
        }
        Destroy(gameObject);
    }
    
}
