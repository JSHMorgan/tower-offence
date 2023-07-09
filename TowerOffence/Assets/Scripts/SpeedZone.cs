using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedZone : Zone
{
    [SerializeField] private float speedUpValue;
    [SerializeField] private float speedUpTime;

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

            if (unitComponent.HasSpeedUp)
            {
                continue;
            }

            StartCoroutine(UnitSpeedUp(unitComponent));
        }
    }

    internal IEnumerator UnitSpeedUp(Unit unit)
    {
        unit.HasSpeedUp = true;
        unit.Speed += speedUpValue;
        yield return new WaitForSeconds(speedUpTime);
        unit.HasSpeedUp = false;
        unit.Speed -= speedUpValue;
    }
}
