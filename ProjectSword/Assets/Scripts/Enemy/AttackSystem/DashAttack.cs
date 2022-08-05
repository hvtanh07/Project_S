using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TrailRenderer))]
[RequireComponent(typeof(Collider2D))]
public class DashAttack : Attack
{
    public float dashDistance;
    public int damage;
    public float dashSpeed;
    public LayerMask wallmask;
    TrailRenderer trail;
    bool Dashing;
    private void Start() {
        trail = GetComponent<TrailRenderer>(); 
    }
    public override void Attacking(Vector3 target){
        Vector3 dir = (target - transform.position).normalized * dashDistance;
        //set target
        Vector3 finaltarget = new Vector2(transform.position.x + dir.x , transform.position.y + dir.y);
        
        //draw line to front to check if dash will hit anything then dash to the target                   
        RaycastHit2D hit = Physics2D.Linecast(transform.position, finaltarget, wallmask);
        if(hit.collider != null){
            finaltarget = hit.point;
        }
        Dashing = true;       
        trail.emitting = true;    
        LeanTween.move(this.gameObject,finaltarget, dashDistance/dashSpeed).setEase(LeanTweenType.easeOutQuart).setOnComplete(FinishedDash);    
    }
    
    void FinishedDash(){
        Dashing = false;
        trail.emitting = false;
        trail.Clear();
    }

    private void OnTriggerEnter2D(Collider2D other) {       
        Player player = other.GetComponent<Player>();
        if(player != null && Dashing){
            player.TakeDamage(damage);
        }
    }
}
