using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoneEnemy : Enemy
{
    [SerializeField] private int _stopSteps = 600; 
    [SerializeField] private float _stopDuration = 7f; 
    [SerializeField] private GameObject _rockPrefab; 
    [SerializeField] private Transform _throwPoint; 
    

    private int _steps = 0; 
    private bool _isStopping = false; 
    private float _stopTime = 0f; 

    private void Update()
    {

        if (!_isStopping)
        {
            Move();
            _steps++;

            if (_steps >= _stopSteps)
            {
                Stop();
            }
        }
        else
        {
            if (Time.time >= _stopTime)
            {
                _isStopping = false;
            }
        }

        CheckDeath();
    }

    private void Stop()
    {
        _isStopping = true;
        _steps = 0;
        _stopTime = Time.time + _stopDuration;
        ThrowRock();
    }

    private void ThrowRock()
    {
        GameObject rock = Instantiate(_rockPrefab, transform.position, Quaternion.identity);
        Rigidbody2D rockRigidbody = rock.GetComponent<Rigidbody2D>();
        rockRigidbody.AddForce(Vector2.left * 500f);
    }
}
