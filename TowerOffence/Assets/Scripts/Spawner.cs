using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private float spawnRateLowerBound = 1.0f;
    [SerializeField] private float spawnRateUpperBound = 3.0f;

    // Start is called before the first frame update
    void Start()
    {
        _ = StartCoroutine(SpawnObject());
    }

    IEnumerator SpawnObject()
    {
        while (true)
        {
            yield return new WaitForSecondsRealtime(Random.Range(spawnRateLowerBound, spawnRateUpperBound));
            Debug.Log("Generate player");
            GameObject newUnit = Instantiate(playerPrefab, transform);
            GameManager.Instance.Units.Add(newUnit);
        }
    }
}
