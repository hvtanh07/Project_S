using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpearHareAttack : Attack
{
    public GameObject spell;
    public float spotWidth;
    public float spotSpreadRange;
    public int numberOfAttackPoint;
    public LayerMask playerMask;
    // Start is called before the first frame update
    public override void Attacking(Vector3 target)
    {
        float angle = 360.0f / numberOfAttackPoint;
        List<Vector3> spotList = new List<Vector3>();
        for (int i = 0; i < numberOfAttackPoint; i++)
        {
            Vector3 offset = Quaternion.Euler(0, 0, angle * i) * Vector3.right * spotSpreadRange;
            Vector3 spot = target + offset;
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
