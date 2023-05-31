using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedSolution : MonoBehaviour
{
    private BoxCollider2D myCollider;

    private void Start()
    {
        myCollider = GetComponent<BoxCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Wall" )
        {
            myCollider.enabled = false;
        }

    }
}
