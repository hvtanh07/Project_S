using System.Collections;
using UnityEngine;


public class DashEnemy : Enemy
{
    [Header("Sword Master stats")]
    public float distanceToDash;
    public float timeBeforeDash;
    public float dashDistance;
    public float dashSpeed;
    public float dashRate = 5.0f; 
    public LayerMask wallmask;
    TrailRenderer trail;
    float currentDashTime;

    // Start is called before the first frame update
    void Start()
    {
        setupAgent();
        agent.stoppingDistance = distanceToDash;
        trail = GetComponent<TrailRenderer>();
        trail.Clear();
        trail.emitting = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(health > 0){
            agent.SetDestination(target.position);
        }
        //float dist=agent.remainingDistance; 
        currentDashTime += Time.deltaTime;
        if (agent.remainingDistance <= agent.stoppingDistance && currentDashTime > dashRate){    
            currentDashTime = 0f;      
            agent.speed = 0;
            trail.emitting = true;
            StartCoroutine(Dash(target.position));
            //prepare to dash
        }     
    }
    IEnumerator Dash(Vector3 target){
        yield return new WaitForSeconds(timeBeforeDash);
        if(health > 0){
            Vector3 dir = (target - transform.position).normalized * dashDistance;
            //set target
            Vector3 finaltarget = new Vector2(transform.position.x + dir.x , transform.position.y + dir.y);
        
            //draw line to front to check if dash will hit anything then dash to the target                   
            RaycastHit2D hit = Physics2D.Linecast(transform.position, finaltarget, wallmask);
            if(hit.collider != null){
                Debug.Log("this one goes outside");
                finaltarget = hit.point;
            }  
            Debug.DrawLine(transform.position,finaltarget,Color.red,1.0f);        
            LeanTween.move(this.gameObject,finaltarget, dashDistance/dashSpeed).setEase(LeanTweenType.easeOutQuart).setOnComplete(FinishedDash);
        }      
        
    }
    
    void FinishedDash(){
        trail.emitting = false;
        trail.Clear();
        StartCoroutine(StartMove(2f));
    }

    IEnumerator StartMove(float time){
        yield return new WaitForSeconds(time);
        if(health > 0){
            agent.speed = speed;
            flinch = false;
        }
        
    }
   
    private void Death(){
        Debug.Log("died");
    }
    private void OnTriggerEnter2D(Collider2D other) {       
        Player player = other.GetComponent<Player>();
        if(player != null && !flinch){
            Debug.Log(other.gameObject.name);
            player.TakeDamage(damage);
        }
    }
}
