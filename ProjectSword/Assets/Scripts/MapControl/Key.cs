using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    public enum KeyType{
        Red,
        Blue,
        Green
    }
    [SerializeField]
    private KeyType keyType;
    private SpriteRenderer sprite;
    private float Transparency;
    private bool Acquired;

    public string SoundName;
    
    private void Awake() {
        Transparency = 1;
        Acquired = false;
        sprite = GetComponent<SpriteRenderer>();
    }
    public void AcquireKey(){
        //FindObjectOfType<AudioManager>().PlaySound(SoundName);
        Acquired = true;
        GetComponent<Collider2D>().enabled = false;
        Destroy(gameObject,5f);
    }
    public KeyType GetKeyType(){
        return keyType;
    }
    private void Update() {     
        if(Acquired){
            sprite.color = new Color(1,1,1,Transparency-=Time.deltaTime);
            transform.position += Vector3.up * Time.deltaTime;
        }
    }
}
