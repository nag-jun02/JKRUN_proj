using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleCharaMove : MonoBehaviour
{
    public float speed = 6;
    
    void Update()
    {
        transform.position += new Vector3(0, 0, speed) * Time.deltaTime;
    }
}
