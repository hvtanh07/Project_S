using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TrailRenderer))]
[RequireComponent(typeof(Collider2D))]
public class MultipleDashAttack : Attack
{
    public int damage;
    public float dashSpeed;
    public List<Vector3> patern;
    TrailRenderer trail;
    bool Dashing;
    int CurrentPaternIndex;
    Vector3 targetAnchor;
    Vector3 originalPos;
    private void Start() {
        trail = GetComponent<TrailRenderer>();
        //patern.Add(transform.position);
        CurrentPaternIndex = 0;
    }
    public override void Attacking(Vector3 target){
        Dashing = true;       
        trail.emitting = true;
        targetAnchor = target;
        originalPos = transform.position;
        Dash(targetAnchor + patern[CurrentPaternIndex]);
    }

    void Dash(Vector3 target){
        LeanTween.move(this.gameObject,target,(target-transform.position).magnitude/dashSpeed).setOnComplete(FinishedDash);
    }
    void FinishedDash(){
        CurrentPaternIndex++;
        if(CurrentPaternIndex > patern.Count -1){
            LeanTween.move(this.gameObject,originalPos,(originalPos-transform.position).magnitude/dashSpeed);
            CurrentPaternIndex = 0;
            Dashing = false;
            trail.emitting = false;
            trail.Clear();
        }else{
            Dash(targetAnchor + patern[CurrentPaternIndex]);
        } 
    }
    private void OnTriggerEnter2D(Collider2D other) {       
        Player player = other.GetComponent<Player>();
        if(player != null && Dashing){
            player.TakeDamage(damage);
        }
    }
}
