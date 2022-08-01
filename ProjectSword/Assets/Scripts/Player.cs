using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public TrailRenderer trail;
    public Joystick joystick;
    [Header("Health")]
    public int health;
    [Header("Attack Stats")]
    public int damage;
    [Header("Movement Stats")]
    [SerializeField] GameObject indicator;
    public float dashDistance;
    public float speed;
    public float maxDashs;
    public float numOfDashs;
    public float dashHealTime = 1;
    public float turnningSpeed;
    public float walkingSpeed;
    public LayerMask wallmask;
    Vector2 dir;
    private bool HoldingDown,Dashing;
    private Rigidbody2D rb;
    private float timer;
    private float lastDashTime = -1;
    private void Start() {
        numOfDashs = maxDashs;
        rb = GetComponent<Rigidbody2D>();
    }
    
    void Update()
    {
        if(HoldingDown){  
            indicator.SetActive(true); 
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
        indicator.SetActive(false);
        if(numOfDashs > 0){
            //get trail and prepare to draw
            trail.transform.SetParent(this.transform);
            trail.transform.localPosition = Vector3.zero;
            trail.Clear();

            //set target
            Vector3 target = new Vector2(transform.position.x + dir.x , transform.position.y + dir.y);

            //draw line to front to check if dash will hit anything then dash to the target                   
            RaycastHit2D hit = Physics2D.Raycast(transform.position, dir, dashDistance, wallmask);
            if(hit.collider != null){
                target = hit.point;
            }          
            Dashing = true;
            
            LeanTween.move(this.gameObject,target, dashDistance/speed).setEase(LeanTweenType.easeOutQuart).setOnComplete(FinishedDash);
        }       
    }
    public void Walk(){
        Quaternion toRotation = Quaternion.LookRotation(indicator.transform.forward, dir);
        indicator.transform.rotation = Quaternion.RotateTowards(indicator.transform.rotation, toRotation, turnningSpeed);

        Vector2 walkoffset = joystick.Direction.normalized * walkingSpeed * Time.deltaTime;
        //transform.position += walkoffset;
        rb.position += walkoffset;	
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
    public int getDamage(){
        return damage;
    }
    public void TakeDamage(int damage){
        if(!Dashing){
            health -= damage;
        }        
        if (health < 0){
            //die
        }
    }
}
