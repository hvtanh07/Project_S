using System;
using System.Collections.Generic;
using UnityEngine;

public class ColliderTrigger : MonoBehaviour
{
    public event EventHandler OnPlayerEnterTrigger;
    private void OnTriggerEnter2D(Collider2D other) {
        Player player = other.GetComponent<Player>();
        if(player != null){
            //Player inside
            OnPlayerEnterTrigger?.Invoke(this, EventArgs.Empty);
        }
    }
}
