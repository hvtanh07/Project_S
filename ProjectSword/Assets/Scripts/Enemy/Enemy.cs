using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;

public class Enemy : MonoBehaviour
{
    //state machine
    private Vector3 startingPosition;
    public event EventHandler OnEnemyDie;
    [HideInInspector] protected Transform target;
    [HideInInspector] protected Vector3 targetAttackPoint;
    public int health;
    public float speed;

    //protected NavMeshAgent agent;
    // Start is called before the first frame update

    
    virtual protected void Death(){
        //agent.speed = 0;
        OnEnemyDie?.Invoke(this, EventArgs.Empty);
        GetComponent<BoxCollider2D>().enabled = false;
        Destroy(gameObject, 3.0f);
    }
    public void Spawn(){
        gameObject.SetActive(true);
    }
    
    virtual public void TakeDamage(int damage){

    }  
}
