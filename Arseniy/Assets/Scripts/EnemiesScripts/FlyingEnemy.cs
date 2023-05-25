using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlyingEnemy : Enemy
{
    [Header("----------FLY PROPERTIES----------")]
    [SerializeField] float amplitude = 1f;
    Vector3 startPosition;


    

    private void Start()
    {
        startPosition = transform.position;
    }
    public override void Move()
    {
        transform.position = startPosition + new Vector3(-1 * Time.time * speed, amplitude * Mathf.Sin(Time.time), 0);   
    }
}

