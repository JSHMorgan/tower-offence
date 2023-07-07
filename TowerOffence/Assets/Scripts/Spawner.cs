using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject playerPrefab;

    // Start is called before the first frame update
    void Start()
    {
        _ = StartCoroutine(SpawnObject());
    }

    IEnumerator SpawnObject()
    {
        while (true)
        {
            yield return new WaitForSecondsRealtime(Random.Range(2.0f, 3.5f));
            Instantiate(playerPrefab, transform.position, Quaternion.identity);
            Debug.Log("Generate player");
        }
    }
}
