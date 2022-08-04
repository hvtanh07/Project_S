using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileAttack : Attack
{
    [SerializeField] GameObject objBullet;
    public float Force;
    public int damage;

    // Update is called once per frame
    public override void Attacking(Vector3 target){
        Quaternion rotation = Quaternion.LookRotation(transform.forward, target - transform.position); 
        GameObject bullet = Instantiate(objBullet,transform.position,rotation);   

        Vector2 dir = transform.TransformDirection(bullet.transform.up * Force);
        bullet.GetComponent<Projectile>().damage = damage;
        bullet.GetComponent<Rigidbody2D>().AddForce(dir,ForceMode2D.Impulse); 
    }
}
