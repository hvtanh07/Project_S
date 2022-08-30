using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpreadKunai : SpecialAttack
{
    public int numOfKunai;
    public float throwForce;
    public int damage;
    [SerializeField] GameObject kunai;
    // Start is called before the first frame update
    public void Attack(){
        float angle = 360.0f / numOfKunai;
        for (int i =0; i< numOfKunai; i++){             
            Quaternion dir = Quaternion.Euler(0, 0, angle * i);   
            GameObject thorwKunai = Instantiate(kunai,transform.position,dir);

            Vector2 kunaiDir = transform.TransformDirection(thorwKunai.transform.up * throwForce);
            thorwKunai.GetComponent<Kunai>().damage = damage;
            thorwKunai.GetComponent<Rigidbody2D>().AddForce(kunaiDir,ForceMode2D.Impulse);
        }
        
    }
}
