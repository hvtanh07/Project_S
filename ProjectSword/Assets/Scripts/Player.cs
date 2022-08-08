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
    private bool m_FacingRight = true;
    private Rigidbody2D rb;
    private Animator anim;
    private float timer;
    private float lastDashTime = -1;
    private void Start() {
        numOfDashs = maxDashs;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
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
            anim.Play("Attack");
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
        anim.Play("Run");
        Quaternion toRotation = Quaternion.LookRotation(indicator.transform.forward, dir);
        indicator.transform.rotation = Quaternion.RotateTowards(indicator.transform.rotation, toRotation, turnningSpeed);

        Vector2 walkoffset = joystick.Direction.normalized * walkingSpeed * Time.deltaTime;
        //transform.position += walkoffset;
        rb.position += walkoffset;	

        if (joystick.Direction.x > 0 && !m_FacingRight)
		{
			Flip();
		}
		// Otherwise if the input is moving the player left and the player is facing right...
		else if (joystick.Direction.x < 0 && m_FacingRight)
		{
			Flip();
		}
    }
    private void FinishedDash(){
        anim.Play("Idle");
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
        anim.Play("Hurt");
        if(!Dashing){
            health -= damage;
        }        
        if (health < 0){
            //die
        }
    }
    public void AnimHurtDone(){
        anim.Play("Idle");
    }

    private void Flip()
	{
		// Switch the way the player is labelled as facing.
		m_FacingRight = !m_FacingRight;

		// Multiply the player's x local scale by -1.
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
}
