using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailControl : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.CompareTag("Enemy")){
            StartCoroutine(Attack(0.5f));
        }
    }

    IEnumerator Attack(float secs)
    {
        yield return new WaitForSeconds(secs);
        Debug.Log("hit");
    }
}
