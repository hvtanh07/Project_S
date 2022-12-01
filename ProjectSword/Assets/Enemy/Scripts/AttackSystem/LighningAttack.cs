using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LighningAttack : Attack
{
    public GameObject lightning;
    public float spotWidth;
    public float spotSpreadRange;
    public int numberOfAttackPoint;
    public LayerMask playerMask;
    // Start is called before the first frame update
    public override void Attacking(Vector3 target)
    {
        float minXRange = target.x - spotSpreadRange;
        float maxXRange = target.x + spotSpreadRange;
        float minYRange = target.y - spotSpreadRange;
        float maxYRange = target.y + spotSpreadRange;

        List<Vector2> randomSpot = new List<Vector2>();
        for (int i = 0; i < numberOfAttackPoint; i++)
        {
            Vector2 Spot = new Vector2(Random.Range(minXRange, maxXRange), Random.Range(minYRange, maxYRange));
            randomSpot.Add(Spot);
        }
        foreach (Vector2 spot in randomSpot)
        {
            Collider2D hitEnemies = Physics2D.OverlapCircle(spot, spotWidth, playerMask);
            GameObject lightningObject = Instantiate(lightning, spot, Quaternion.identity);
            Destroy(lightningObject, 0.8f);
            if (hitEnemies != null)
            {
                hitEnemies.GetComponent<Player>().TakeDamage(damage);
            }
        }
    }
}
