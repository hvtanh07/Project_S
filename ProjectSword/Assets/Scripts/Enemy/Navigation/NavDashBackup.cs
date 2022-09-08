using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavDashBackup : Navigation
{
    public float SpeedMultiplier;
    public float distanceToEvade;
    public LayerMask wallMask;
    public float dashCoolDown;
    Vector3 finalTarget;
    bool Dashing;
    [SerializeField] float lastDash;
    private void Update() {
        lastDash += Time.deltaTime;
    }

    private void Start()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
    }
    private void Dash(Vector3 target)
    {
        float speed = SpeedMultiplier * GetComponent<Enemy>().speed;
        //set target
        Vector3 finaltarget = target;

        //draw line to front to check if dash will hit anything then dash to the target                   
        RaycastHit2D hit = Physics2D.Linecast(transform.position, finaltarget, wallMask);
        if (hit.collider != null)
        {
            finaltarget = hit.point;
        }
        float dashDistance = (finalTarget - transform.position).magnitude;

        LeanTween.move(this.gameObject, finaltarget, dashDistance / speed).setEase(LeanTweenType.easeOutQuart).setOnComplete(FinishedDash);
    }
    void FinishedDash()
    {
        lastDash = 0f;
        Dashing = false;
    }
    public override bool Navigating(Transform target, float distanceToAttack)
    {
        if (target != null)
        {
            var dir = transform.position - target.position;
            var distance = dir.magnitude;
            finalTarget = target.position;
            if (distance < distanceToEvade && !Dashing && lastDash > dashCoolDown)
            {
                Dashing = true;
                finalTarget = target.position + dir.normalized * (distanceToAttack + 2f);
                Dash(finalTarget);
            }
            //else
            //{
            //    Dashing = false;
            //}

            agent.destination = finalTarget;

            if (agent.velocity.x > 0 && !m_FacingRight)
            {
                Flip();
            }
            else if (agent.velocity.x < 0 && m_FacingRight)
            {
                Flip();
            }

            if (agent.remainingDistance <= agent.stoppingDistance && !agent.pathPending && !Dashing)
            {
                agent.SetDestination(gameObject.transform.position);
                if (target.position.x - transform.position.x > 0 && !m_FacingRight)
                {
                    Flip();
                }
                else if (target.position.x - transform.position.x < 0 && m_FacingRight)
                {
                    Flip();
                }
                return false;
            }
            else if (agent.remainingDistance > agent.stoppingDistance || Dashing)
            {
                return true;
            }
            return true;
        }
        else
        {
            return false;
        }
    }
}
