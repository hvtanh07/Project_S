using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : Defend
{
    public int maximumBlockHealth;
    public int blockingHealth;
    public int healAmount;
    public float blockHealTime = 1;
    private float lastBlockTime = -1;
    // Start is called before the first frame update
    void Start()
    {
        blockingHealth = maximumBlockHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (blockingHealth < maximumBlockHealth && Time.time - lastBlockTime >= blockHealTime)
        {
            blockingHealth += healAmount;
            if (blockingHealth > maximumBlockHealth)
                blockingHealth = maximumBlockHealth;
                
            lastBlockTime = Time.time;
        }
    }

    public override int Defending(int damage)
    {
        if (blockingHealth <= 0)
        {
            return damage;
            //StartCoroutine(Hurt(damage));  
        }
        else
        {
            blockingHealth -= damage;
            lastBlockTime = Time.time;
            return 0;
        }
    }
}
