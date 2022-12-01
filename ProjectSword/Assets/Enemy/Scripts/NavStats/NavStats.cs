using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum NavType
{
    Dash,
    Walk,
    KeepDistance
}
public abstract class NavStats : MonoBehaviour
{
    public NavType type;
    public bool unlocked = false;
}
