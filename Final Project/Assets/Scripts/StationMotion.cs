using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StationMotion : MonoBehaviour
{
    public float speed;
    
    void Update()
    {
        transform.Rotate(0f, speed, 0f);
    }
}
