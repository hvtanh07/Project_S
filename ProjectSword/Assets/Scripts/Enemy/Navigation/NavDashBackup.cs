using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavDashBackup : Navigation
{
    private float SpeedMultiplier;
    public float distanceToEvade;
    public LayerMask wallMask;
    public float dashCoolDown;
    Vector3 finalTarget;
    Vector3 dashTarget;
    bool Dashing;
    [SerializeField] float lastDash;

    private void Start()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
    }
    private void Dash(Vector3 target)
    {
        float speed = SpeedMultiplier * GetComponent<Enemy>().speed;
        //set target
        dashTarget = target;

        //draw line to front to check if dash will hit anything then dash to the target                   
        RaycastHit2D hit = Physics2D.Linecast(transform.position, dashTarget, wallMask);
        if (hit.collider != null)
        {
            Debug.Log(hit.collider.gameObject.name);
            dashTarget = hit.point;
        }
        float dashDistance = (dashTarget - transform.position).magnitude;
        LeanTween.move(gameObject, dashTarget, dashDistance / speed).setOnComplete(FinishedDash);
        //FinishedDash();
    }
    void FinishedDash()
    {
        Debug.Log("Finished Dash");
        lastDash = 0f;
        Dashing = false;
    }
    public override bool Navigating(Transform target, float distanceToAttack)
    {
        lastDash += Time.deltaTime;
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
    private void OnDrawGizmos() {
        Gizmos.DrawSphere(dashTarget,0.5f);
    }
}
