using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Healing : SpecialAttack
{
    int healAmount;
    float healthSpeed;
    float lastHeal;
    bool actived = false;

    private void Update() {
        if (actived && Time.time - lastHeal > healthSpeed)
        {
            GetComponent<Player>().Healing(healAmount);

            lastHeal = Time.time;
        }
    }

    public override void Attack(Vector2 dir){
        actived = false;
        lastHeal = Time.time;
    }
}
