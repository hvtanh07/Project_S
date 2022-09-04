using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavRunToTarget : Navigation
{
    private void Start() {
        agent = GetComponent<NavMeshAgent>();
    }
    public override bool Navigating(Transform target, float distanceToAttack)
    {
        if(target != null){
            agent.destination = target.position;                       
            //anim.SetBool("Moving", true);
            if (agent.velocity.x > 0 && !m_FacingRight)
		    {
			    Flip();
		    }
		    else if (agent.velocity.x < 0 && m_FacingRight)
		    {
			    Flip();
		    }

            if (agent.remainingDistance <= agent.stoppingDistance && !agent.pathPending){
                return false;
            }
            else if (agent.remainingDistance > agent.stoppingDistance){
                return true;
            }
            return true;
        }else{
            return false;
        }
    }
}
