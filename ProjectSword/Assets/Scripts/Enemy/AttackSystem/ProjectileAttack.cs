using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileAttack : Attack
{
    public GameObject objBullet;
    public float Force;

    [Range(0, 3)]
    public int numOfSideProjectiles;
    public int spreadAngle;

    // Update is called once per frame
    public override void Attacking(Vector3 target){
        if (numOfSideProjectiles > 0){
            ShootSpread(target);
        } else if(numOfSideProjectiles == 0){
            Shoot(target);
        } else{
            return;
        }
    }

    private void ShootSpread(Vector3 target){
        Quaternion rotation = Quaternion.LookRotation(transform.forward, target - transform.position);
        for (int i = -numOfSideProjectiles; i<=numOfSideProjectiles ; i++){
            GameObject bullet = Instantiate(objBullet,transform.position,rotation * Quaternion.Euler(0,0, i * spreadAngle));

            Vector2 dir = transform.TransformDirection(bullet.transform.up * Force);
            bullet.GetComponent<Projectile>().damage = damage;
            bullet.GetComponent<Rigidbody2D>().AddForce(dir,ForceMode2D.Impulse);
        }
    }

    private void Shoot(Vector3 target){
        Quaternion rotation = Quaternion.LookRotation(transform.forward, target - transform.position); 
        GameObject bullet = Instantiate(objBullet,transform.position,rotation);   

        Vector2 dir = transform.TransformDirection(bullet.transform.up * Force);
        bullet.GetComponent<Projectile>().damage = damage;
        bullet.GetComponent<Rigidbody2D>().AddForce(dir,ForceMode2D.Impulse);
    }
}
