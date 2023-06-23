using System.Collections;
using UnityEngine;

public class TempSpawner : MonoBehaviour
{
    public GameObject[] objectsArray1;
    public GameObject[] objectsArray2;

    private WaitForSeconds initialDelay = new WaitForSeconds(2f);
    private WaitForSeconds secondCycleDelay = new WaitForSeconds(5f);

    private void Start()
    {
        StartCoroutine(SpawnObjectsFirstCycle());
    }

    private IEnumerator SpawnObjectsFirstCycle()
    {
        for (int i = 0; i < 2; i++)
        {
            yield return initialDelay;

            int randomIndex = Random.Range(0, objectsArray1.Length);
            GameObject newObject = Instantiate(objectsArray1[randomIndex], transform.position, Quaternion.identity);

        }

        StartCoroutine(SpawnObjectsSecondCycle());
    }

    private IEnumerator SpawnObjectsSecondCycle()
    {
        yield return new WaitForSeconds(5f);

        while (true)
        {
            yield return secondCycleDelay;

            int randomIndex = Random.Range(0, objectsArray2.Length);

            float randomY = Random.Range(-5f, 5f);
            Vector3 spawnPosition = new Vector3(transform.position.x, randomY, transform.position.z);

            GameObject newObject = Instantiate(objectsArray2[randomIndex], spawnPosition, Quaternion.identity);
        }
    }
}

