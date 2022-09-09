using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockAttack : Attack
{
    public GameObject spell;
    public float spotWidth;
    public float spotSpreadRange;
    public int numberOfAttackPoint;
    public LayerMask playerMask;
    // Start is called before the first frame update
    public override void Attacking(Vector3 target)
    {
        int halfLine = numberOfAttackPoint / 2;
        Vector3 dir = (target - transform.position).normalized;
        List<Vector3> spotList = new List<Vector3>();
        for (int i = -halfLine; i <= halfLine; i++)
        {
            Vector3 spot = target + dir * i;
            spotList.Add(spot);
        }
        foreach (Vector2 spot in spotList)
        {
            GameObject lightningObject = Instantiate(spell, spot, Quaternion.identity);
            Destroy(lightningObject, 0.8f);
            Collider2D hitEnemies = Physics2D.OverlapCircle(spot, spotWidth, playerMask);
            if (hitEnemies != null)
            {
                hitEnemies.GetComponent<Player>().TakeDamage(damage);
            }
        }
    }
}

