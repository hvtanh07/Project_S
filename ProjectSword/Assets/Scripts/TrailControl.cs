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
        if(other.gameObject.CompareTag("Enemy")){
            StartCoroutine(Attack(0.5f, slashDamage, other));
        }
    }

    IEnumerator Attack(float secs, int damage, Collider2D col)
    {
        yield return new WaitForSeconds(secs);
        Enemy enemy = col.GetComponent<Enemy>();
        if (enemy != null)
            enemy.gameObject.GetComponent<Enemy>().TakeDamage(damage);
    }
}
