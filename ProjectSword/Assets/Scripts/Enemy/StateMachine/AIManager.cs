using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(0)]
public class AIManager : MonoBehaviour
{
    private static AIManager instance;
    // Start is called before the first frame update
    public static AIManager Instance{
        get{
            return instance;
        }
        private set{
            instance = value;
        }
    }

    public Transform target;
    public float RadiusAroundTarget = 0.5f;

    public List<Enemy> Units = new List<Enemy>();

    private void Awake() {
        if(Instance == null)
        {
            Instance = this;
            return;
        }

        Destroy(gameObject);
    }

    private void MakeEnemyCircleTarget(){
        for(int i = 0; i < Units.Count; i++){
            Vector3 targetPos = new Vector3(
                target.position.x + RadiusAroundTarget * Mathf.Cos(2 * Mathf.PI * i/Units.Count),
                target.position.y + RadiusAroundTarget * Mathf.Cos(2 * Mathf.PI * i/Units.Count),
                target.position.z);
        }
    }
}
