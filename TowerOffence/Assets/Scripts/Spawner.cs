using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject[] playerPrefabs;
    [SerializeField] private float spawnRateLowerBound = 1.0f;
    [SerializeField] private float spawnRateUpperBound = 3.0f;

    // Start is called before the first frame update
    void Start()
    {
        _ = StartCoroutine(SpawnObject());
    }

    private IEnumerator SpawnObject()
    {
        while (true)
        {
            yield return new WaitForSecondsRealtime(Random.Range(spawnRateLowerBound, spawnRateUpperBound));
            Debug.Log("Generate player");
            GameObject newUnit = Instantiate(playerPrefabs[Random.Range(0, playerPrefabs.Length)], transform);
            GameManager.Instance.Units.Add(newUnit);
        }
    }
}
