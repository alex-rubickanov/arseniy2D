using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    [SerializeField] private List<GameObject> _enemies;
    [SerializeField] private int _timeDelay;
    [SerializeField] private float _minY;
    [SerializeField] private float _maxY;

    void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

     private IEnumerator SpawnEnemies()
     {
        for (int i = 0; i < _enemies.Count; i++)
        {
            yield return new WaitForSeconds(_timeDelay);
            Instantiate(_enemies[i], RandomPosition(), Quaternion.identity);
        }
     }

    private Vector3 RandomPosition()
    {
        return new Vector3(transform.position.x, Random.Range(_minY, _maxY), transform.position.z);
    }
}
