using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodeOnDeath : Defend
{
    public GameObject explosion;
    public int maximumDamage;
    public float radius;
    
    public override int Defending(int damage){
        Collider2D[] collidersToAttack = Physics2D.OverlapCircleAll(new Vector2(transform.position.x, transform.position.y), radius);
        //Instantiate(explosion,transform.position, Quaternion.identity);
        
        foreach (Collider2D hitTarget in collidersToAttack)
        {
            var explosionDistance = (hitTarget.transform.position - transform.position).magnitude;
            float effectiveRange;
            switch (hitTarget.gameObject.tag)
            {
                case "Player":
                    {
                        Player player = hitTarget.GetComponent<Player>();
                        if (player != null)
                        {

                            if (explosionDistance > 1)
                            {
                                effectiveRange = 1 / explosionDistance;
                            }
                            else
                            {
                                effectiveRange = 1;
                            }
                            player.TakeDamage(Mathf.RoundToInt(effectiveRange * maximumDamage));
                        }
                        break;
                    }
                case "Enemy":
                    {
                        Enemy enemy = hitTarget.GetComponent<Enemy>();
                        if (enemy != null && enemy != GetComponent<Enemy>())
                        {
                            Debug.Log(hitTarget.gameObject.name);
                            if (explosionDistance > 1)
                            {
                                effectiveRange = 1 / explosionDistance;
                            }
                            else
                            {
                                effectiveRange = 1;
                            }
                            enemy.InstantTakeDamage(Mathf.RoundToInt(effectiveRange * maximumDamage));
                        }
                        break;
                    }
            }

        }
        return 100;
        //draw death anim as explode monkey
    }
}
