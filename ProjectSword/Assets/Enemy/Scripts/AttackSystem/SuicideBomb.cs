using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuicideBomb : Attack
{
    public GameObject explosion;
    public float radius;

    public override void Attacking(Vector3 target)
    {

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
                            player.TakeDamage(Mathf.RoundToInt(effectiveRange * damage));
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
                            enemy.InstantTakeDamage(Mathf.RoundToInt(effectiveRange * damage));
                        }
                        break;
                    }
            }
        }
        BattleSystem.instance.enemyKilled();
        Destroy(gameObject);
    }
}
