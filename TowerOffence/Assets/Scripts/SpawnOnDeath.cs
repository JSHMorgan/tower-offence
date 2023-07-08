using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnOnDeath : MonoBehaviour
{
    [SerializeField] private GameObject playerPrefab;

    private void Update()
    {
        if (GetComponent<Unit>().Health == 0)
        {
            PointsBasedMovement pointsBasedMovement = gameObject.GetComponent<PointsBasedMovement>();
            int currentPointTarget = pointsBasedMovement.CurrentPointTarget;
            int spawnPoint = currentPointTarget - Random.Range(1, currentPointTarget);

            GameObject newUnit = Instantiate(playerPrefab, pointsBasedMovement.Points[spawnPoint]);
            newUnit.GetComponent<PointsBasedMovement>().CurrentPointTarget = spawnPoint + 1;

            GameManager.Instance.Units.Add(newUnit);
            Debug.Log(newUnit);
        }
    }
}