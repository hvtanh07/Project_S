using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyHolder : MonoBehaviour
{
    private List<Key.KeyType> keyList;

    private void Awake() {
        keyList = new List<Key.KeyType>();
    }

    public void AddKey(Key.KeyType key){
        keyList.Add(key);
    }

    public void UseKey(Key.KeyType key){
        keyList.Remove(key);
    }

    public bool searchKey(Key.KeyType key){
        return keyList.Contains(key);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        Key key = other.GetComponent<Key>();
        if(key != null){
            AddKey(key.GetKeyType());
            key.AcquireKey();
            
        }

        KeyDoor keydoor = other.GetComponent<KeyDoor>();
        if (keydoor != null){
            if(searchKey(keydoor.GetKeyType())){
                UseKey(keydoor.GetKeyType());
                keydoor.OpenDoor();               
            }
        }
    }
}
