using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject indicator;
    public Joystick joystick;
    public int health;
    public float turnningSpeed;
    public float walkingSpeed;
    [HideInInspector] public bool HoldingDown, dashing;
    private bool m_FacingRight = true;
    [HideInInspector] public Vector2 dir;
    private Rigidbody2D rb;
    private Animator anim;
    [HideInInspector] public bool walkable;

    private void Start()
    {
        walkable = true;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (HoldingDown && !dashing)
        {
            anim.ResetTrigger("Attack");
            indicator.SetActive(true);
            dir = joystick.Direction.normalized;
            Walk();
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            Time.timeScale = 0.1f;
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            Time.timeScale = 1f;
        }
    }

    public void GetDir()
    {
        HoldingDown = true;
    }


    public void Walk()
    {
        if (walkable && joystick.Direction.magnitude > 0f)
        {
            Quaternion toRotation = Quaternion.LookRotation(indicator.transform.forward, dir);
            indicator.transform.rotation = Quaternion.RotateTowards(indicator.transform.rotation, toRotation, turnningSpeed);
            if (joystick.Direction.magnitude > 0.5f)
            {
                anim.SetBool("Running", true);

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
            else
            {
                anim.SetBool("Running", false);
            }
        }
    }


    public void TakeDamage(int damage)
    {
        if (!dashing)
        {
            walkable = false;
            anim.SetTrigger("Hurt");
            health -= damage;
        }
        if (health < 0)
        {
            //die
        }
    }

    public void AnimHurtDone()
    {
        walkable = true;
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
