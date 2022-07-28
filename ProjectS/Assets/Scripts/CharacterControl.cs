using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CharacterControl : MonoBehaviour
{
    [Header("Movement")]
    public Joystick joystick;
    public float dashDistance;
    public float speed;
    public float maxDashs;
    public float numOfDashs;
    public float dashHealTime = 2;
    [Header("Attack")]
    public float damage;
    Vector2 dir;
    private bool HoldingDown,Dashing;
    private float timer;
    private float lastDashTime = -1;
    private void Start() {
        numOfDashs = maxDashs;
    }

    // Update is called once per frame
    void Update()
    {    
        if(HoldingDown && joystick.Direction != Vector2.zero){
            dir = joystick.Direction.normalized * dashDistance;
        }
        if ( lastDashTime >= 0 && Time.time - lastDashTime >= dashHealTime && numOfDashs < maxDashs )
        {
            addDash();
        }
         if (Input.GetKeyDown(KeyCode.R)){
            Time.timeScale = 0.1f;
        }
         if (Input.GetKeyDown(KeyCode.E)){
            Time.timeScale = 1f;
        }
    }
    
    public void GetDir(){
        HoldingDown = true;      
    }
    public void Push(){
        HoldingDown = false;
        if(numOfDashs > 0){
            Vector2 target = new Vector2(transform.position.x + dir.x , transform.position.y + dir.y);
            Dashing = true;
            Quaternion toRotation = Quaternion.LookRotation(transform.forward, dir);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, 360f);
            LeanTween.move(this.gameObject,target, dashDistance/speed).setOnComplete(FinishedDash);
        }       
       
    }
    
    private void FinishedDash(){
        Dashing = false;
        numOfDashs--;
        lastDashTime = Time.time;
    }
    public void addDash(){              
        numOfDashs++;   
        lastDashTime = Time.time;      
    }
    public float DealtDamage(){
        if(Dashing){
            return damage;
        }
        return 0f;
    }
}
 
    
