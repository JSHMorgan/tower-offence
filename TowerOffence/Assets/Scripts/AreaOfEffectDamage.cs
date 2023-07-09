using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaOfEffectDamage : MonoBehaviour
{
    [SerializeField] private float radius;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        foreach (GameObject unit in GameObject.FindGameObjectsWithTag("Player"))
        {
            bool isVisible = unit.GetComponent<SpriteRenderer>().isVisible;
            bool hasPointsBasedMovement = unit.GetComponent<PointsBasedMovement>() == null;
            bool isWithinRadius = Vector2.Distance(transform.position, unit.transform.position) > radius;

            if (!isVisible || hasPointsBasedMovement || isWithinRadius)
            {
                continue;
            }
        }
    }
}
