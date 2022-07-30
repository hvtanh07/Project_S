using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DashEnemy : MonoBehaviour
{
    [Header("Dash stats")]
    public float distanceToDash;
    public float timeBeforeDash;
    public float dashDistance;
    public float dashSpeed;
    public float dashRate = 5.0f; 
    public LayerMask wallmask;
    TrailRenderer trail;

    [Header("Enemy stats")]
    [SerializeField] Transform target;
    public float flinchTime;
    NavMeshAgent agent;
    public int health;
    public float speed;
    public int damage;
    float attackRate = 1.0f; 
    [SerializeField]float currentDamageTime; 
    [SerializeField]float currentDashTime;
    private bool flinch;
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        agent.speed = speed;
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        agent.stoppingDistance = distanceToDash;
        trail = GetComponent<TrailRenderer>();
        trail.Clear();
        trail.emitting = false;
        //agent.obstacleAvoidanceType = ObstacleAvoidanceType.HighQualityObstacleAvoidance;
    }

    // Update is called once per frame
    void Update()
    {
        agent.SetDestination(target.position);
        //float dist=agent.remainingDistance; 
        currentDashTime += Time.deltaTime;
        if (agent.remainingDistance <= agent.stoppingDistance && currentDashTime > dashRate && health > 0){    
            currentDashTime = 0f;      
            agent.speed = 0;
            trail.emitting = true;
            StartCoroutine(Dash(target.position));
            //prepare to dash
        }     
    }
    IEnumerator Dash(Vector3 target){
        yield return new WaitForSeconds(timeBeforeDash);      
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
        //Dashing = true;
        Quaternion toRotation = Quaternion.LookRotation(transform.forward, dir);
        transform.rotation = toRotation;
        LeanTween.move(this.gameObject,finaltarget, dashDistance/dashSpeed).setEase(LeanTweenType.easeOutQuart).setOnComplete(FinishedDash);
    }
    public void TakeDamage(int damage){
        flinch = true;
        agent.speed = 0;
        StartCoroutine(Hurt(damage));       
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

    IEnumerator Hurt(int damage)
    {             
        yield return new WaitForSeconds(flinchTime);
        health -= damage;
        if(health <=0){
            Death();
        }else {
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
            currentDamageTime = 0.0f;
        }
    }
    private void OnTriggerStay2D(Collider2D other) {       
        Player player = other.GetComponent<Player>();
        if(player != null && !flinch){
            currentDamageTime += Time.deltaTime;
            if(currentDamageTime > attackRate)
            {
                player.TakeDamage(damage);
                currentDamageTime = 0.0f;
            }
        }
    }

}
