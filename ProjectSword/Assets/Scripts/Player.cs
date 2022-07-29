using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public TrailRenderer trail;
    public Joystick joystick;
    public float dashDistance;
    public float speed;
    public float maxDashs;
    public float numOfDashs;
    public float dashHealTime = 1;
    public float turnningSpeed;
    public float walkingSpeed;
    Vector2 dir;
    private bool HoldingDown,Dashing;
    private float timer;
    private float lastDashTime = -1;
    private void Start() {
        numOfDashs = maxDashs;
    }
    
    void Update()
    {
        if(HoldingDown){         
            dir = joystick.Direction.normalized * dashDistance;
            Walk();
        }
        if ( numOfDashs < maxDashs && Time.time - lastDashTime >= dashHealTime )
        {
            addDash();
        }
        if(Input.GetKey(KeyCode.R)){
            Time.timeScale = 0.2f;
        }
        if (Input.GetKey(KeyCode.E))
        {
            Time.timeScale = 1f;
        }
    }
    public void GetDir(){
        HoldingDown = true;      
    }
    public void Dash(){
        HoldingDown = false;
        if(numOfDashs > 0){
            trail.transform.SetParent(this.transform);
            trail.transform.localPosition = Vector3.zero;
            trail.Clear();
            Vector2 target = new Vector2(transform.position.x + dir.x , transform.position.y + dir.y);
            Dashing = true;
            Quaternion toRotation = Quaternion.LookRotation(transform.forward, dir);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, 360f);
            LeanTween.move(this.gameObject,target, dashDistance/speed).setEase(LeanTweenType.easeOutQuart).setOnComplete(FinishedDash);
        }       
    }
    public void Walk(){
        Quaternion toRotation = Quaternion.LookRotation(transform.forward, dir);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, turnningSpeed);
        Vector3 walkoffset = joystick.Direction.normalized * walkingSpeed * Time.deltaTime;
        transform.position += walkoffset;
    }
    private void FinishedDash(){
        //trail.emitting = false;
        trail.transform.SetParent(null);
        trail.Clear();
        Dashing = false;
        numOfDashs--;
        lastDashTime = Time.time;
    }
    public void addDash(){              
        numOfDashs++;   
        lastDashTime = Time.time;      
    }
}
