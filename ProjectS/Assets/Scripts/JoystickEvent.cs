using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class JoystickEvent : MonoBehaviour
{
    private FloatingJoystick joystick;
    [Header("Custome Event")]
    public UnityEvent onJoyDown;
    public UnityEvent onJoyUp;
    // Start is called before the first frame update
   

    // Update is called once per frame
    void Update()
    {
        
    }
}
