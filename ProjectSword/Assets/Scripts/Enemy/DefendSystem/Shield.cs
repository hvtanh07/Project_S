using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    public int maximumBlockHealth;
    int blockingHealth;
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
        if ( blockingHealth < maximumBlockHealth && Time.time - lastBlockTime >= blockHealTime)
        {
            blockingHealth += healAmount;
            lastBlockTime = Time.time;
        }
    }

    public int Block(int damage){
        if (blockingHealth <=0){
            return damage;
            //StartCoroutine(Hurt(damage));  
        }else{
            blockingHealth -= damage;
            lastBlockTime = Time.time;
            return 0;
        }
    }
}
