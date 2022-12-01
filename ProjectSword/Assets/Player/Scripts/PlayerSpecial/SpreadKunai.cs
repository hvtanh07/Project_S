using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpreadKunai : SpecialAttack
{
    public int numOfKunai;
    public float angleBetween;
    public float throwForce;
    public int damage;
    [SerializeField] GameObject kunai;
    // Start is called before the first frame update
    public override void Attack(Vector2 dir){
        float spreadAngle = angleBetween * (numOfKunai - 1);
        float Offset = spreadAngle/2;
        print(Offset);
        Quaternion rotation = Quaternion.LookRotation(transform.forward, dir);
        
        rotation *= Quaternion.Euler(0,0,Offset);
        
        for (int i =0; i< numOfKunai; i++){
           GameObject thorwKunai = Instantiate(kunai,transform.position,rotation * Quaternion.Euler(0,0,i * -angleBetween));
           Vector2 kunaiDir = transform.TransformDirection(thorwKunai.transform.up * throwForce);
           thorwKunai.GetComponent<Kunai>().damage = damage;
           thorwKunai.GetComponent<Rigidbody2D>().AddForce(kunaiDir,ForceMode2D.Impulse);
        }
    }
}
