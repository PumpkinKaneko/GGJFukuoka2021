using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleAirPods : MonoBehaviour
{
    public float RotSpeed = 90f;


    void Start()
    {

    }


    void Update()
    {
        this.transform.Rotate(0, this.RotSpeed * Time.deltaTime, 0);
    }
}
