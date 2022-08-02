using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SpawnerState : MonoBehaviour
{
    public abstract SpawnerState RunCurrentState();
}
