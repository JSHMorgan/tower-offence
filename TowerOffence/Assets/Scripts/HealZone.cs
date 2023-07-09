using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealZone : Zone
{
    [SerializeField] private int healValue;
    [SerializeField] private float timeBetweenHeal;

    // Update is called once per frame
    private void Update()
    {
        foreach (var unit in GameObject.FindGameObjectsWithTag("Player"))
        {
            if (unit == null)
            {
                continue;
            }

            Unit unitComponent = unit.GetComponent<Unit>();

            if (unitComponent.GetDistanceFromPoint(transform.position) > radius)
            {
                continue;
            }

            if (unitComponent.HasHealthUp)
            {
                continue;
            }

            StartCoroutine(UnitHeal(unitComponent));
        }
    }

    internal IEnumerator UnitHeal(Unit unit)
    {
        unit.HasHealthUp = true;
        unit.Health += healValue;
        yield return new WaitForSeconds(timeBetweenHeal);
        unit.HasHealthUp = false;
    }
}
