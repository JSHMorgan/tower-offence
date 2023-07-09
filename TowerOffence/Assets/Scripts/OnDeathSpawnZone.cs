using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnDeathSpawnZone : MonoBehaviour
{
    [SerializeField] private float zoneRadius;
    [SerializeField] private GameObject zonePrefab;
    private void Update()
    {
        if (GetComponent<Unit>().Health <= 0)
        {
            var zone = Instantiate(zonePrefab, transform.position, Quaternion.identity);
            zone.GetComponent<SpriteRenderer>().color = gameObject.GetComponent<SpriteRenderer>().color;
        }
    }
}
