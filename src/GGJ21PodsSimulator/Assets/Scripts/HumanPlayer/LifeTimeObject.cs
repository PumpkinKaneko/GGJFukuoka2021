using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeTimeObject : MonoBehaviour
{
    [SerializeField]
    float m_life_time;

    void Start()
    {
        Invoke("Dead", m_life_time);
    }

    void Dead()
    {
        Destroy(gameObject);
    }
}
