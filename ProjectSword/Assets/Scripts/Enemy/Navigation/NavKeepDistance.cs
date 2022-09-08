using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavKeepDistance : Navigation
{
    Vector3 finalTarget;
    bool backingUp;
    private void Start()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
    }
    public override bool Navigating(Transform target, float distanceToAttack)
    {
        if (target != null)
        {
            var dir = transform.position - target.position;
            var distance = dir.magnitude;
            finalTarget = target.position;
            if (distance < distanceToAttack - 1)
            {
                backingUp = true;
                agent.stoppingDistance = 0;
                finalTarget = new Vector3(
                    target.position.x + distanceToAttack * Mathf.Cos(Vector3.Angle(transform.position, target.position) * Mathf.Deg2Rad),
                    target.position.y + distanceToAttack * Mathf.Sin(Vector3.Angle(transform.position, target.position) * Mathf.Deg2Rad),
                    target.position.z);
            }
            else if (distance >= distanceToAttack - 1)
            {
                backingUp = false;
                agent.stoppingDistance = distanceToAttack;
            }
            agent.destination = finalTarget;

            if (agent.velocity.x > 0 && !m_FacingRight)
            {
                Flip();
            }
            else if (agent.velocity.x < 0 && m_FacingRight)
            {
                Flip();
            }

            if (agent.remainingDistance <= agent.stoppingDistance && !agent.pathPending && !backingUp)
            {
                return false;
            }
            else if (agent.remainingDistance > agent.stoppingDistance || backingUp)
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
