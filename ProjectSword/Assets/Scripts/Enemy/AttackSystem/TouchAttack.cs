using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class TouchAttack : Attack
{
    Player player;

    bool touchingPlayer;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            player = other.GetComponent<Player>();
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            player = null;
        }
    }
    public override void Attacking(Vector3 target)
    {
        if (player != null)
        {
            player.TakeDamage(damage);
        }
    }
}
