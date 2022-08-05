using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyDoor : MonoBehaviour
{
    private AudioSource au;
    public Vector3 moveDirection;
    public bool unlocked;
    public float speed;
    private Vector3 targetDir;
    [SerializeField] private Key.KeyType keyType;
    private void Start() {
        au = GetComponent<AudioSource>();
        unlocked = false;
        targetDir = transform.position + moveDirection;
    }
    public void OpenDoor(){
        unlocked = true;
        //LeanTween.move(this.gameObject,targetDir,speed).setOnComplete(stopAudio);
        au.Play();
       
    }
    public void stopAudio(){
        au.Stop();
    }
    public Key.KeyType GetKeyType(){
        return keyType;
    }
}
