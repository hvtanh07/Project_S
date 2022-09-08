using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public abstract class Navigation : MonoBehaviour
{
    protected bool m_FacingRight = false;
    protected NavMeshAgent agent;
    public abstract bool Navigating(Transform target, float distanceToAttack);
    protected void Flip()
	{
		// Switch the way the player is labelled as facing.
		m_FacingRight = !m_FacingRight;

		// Multiply the player's x local scale by -1.
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
}
