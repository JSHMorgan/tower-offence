using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaOfEffectDamage : MonoBehaviour
{
    [SerializeField] private float radius;
    [SerializeField] private int damage;
    [SerializeField] private float fireRate;

    private bool canUse = true;

    private void Update()
    {
        bool anyWithinRadius = false;
        List<GameObject> unitsInRadius = new();
        foreach (GameObject unit in GameObject.FindGameObjectsWithTag("Player"))
        {
            bool isVisible = unit.GetComponent<SpriteRenderer>().isVisible;
            bool hasPointsBasedMovement = unit.GetComponent<PointsBasedMovement>() != null;
            bool isWithinRadius = Vector2.Distance(transform.position, unit.transform.position) < radius;

            if (!isVisible || !hasPointsBasedMovement || !isWithinRadius)
            {
                continue;
            }

            unitsInRadius.Add(unit);
            anyWithinRadius = true;

            
        }

        if (anyWithinRadius && canUse)
        {
            Debug.Log("Fire");
            canUse = false;
            StartCoroutine(HandleFireRate());
            foreach (var unit in unitsInRadius)
            {
                unit.GetComponent<Unit>().DealDamage(damage);
            }
        }
    }
    IEnumerator HandleFireRate()
    {
        GetComponent<SpriteRenderer>().color = Color.red;
        yield return new WaitForSeconds(1.0f / fireRate);
        canUse = true;
        GetComponent<SpriteRenderer>().color = Color.white;
    }
}
