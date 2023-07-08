using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public enum AimingOption
{
    First,
    Last
}

public class ShootUnit : MonoBehaviour
{
    [SerializeField] private float radius = 5f;
    [SerializeField] private float rotationSpeed = 500f;
    [Tooltip("The number of seconds between each attack fired.")]
    [SerializeField] private float fireRate = 2.0f;
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private AimingOption aimingOption = AimingOption.First;

    private GameObject projectile = null;
    private GameObject target = null;

    private void Start()
    {
        _ = StartCoroutine(FireProjectile());
    }
    // Update is called once per frame
    private void FixedUpdate()
    {
        target = FindUnitToTarget();

        if (target == null)
        {
            return;
        }

        // Make the tower face the unit it is currently firing at.
        float angle = Mathf.Atan2(target.transform.position.y - transform.position.y, target.transform.position.x - transform.position.x) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.Euler(new Vector3(0, 0, angle - 90));
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime);
    }

    private GameObject FindUnitToTarget()
    {
        GameObject tempTarget = null;
        foreach (GameObject unit in GameObject.FindGameObjectsWithTag("Player"))
        {
            bool isVisible = unit.GetComponent<SpriteRenderer>().isVisible;
            bool hasPointsBasedMovement = unit.GetComponent<PointsBasedMovement>() == null;
            bool isWithinRadius = Vector2.Distance(transform.position, unit.transform.position) > radius;

            if (!isVisible || hasPointsBasedMovement || isWithinRadius)
            {
                continue;
            }

            // If the target is null, set it to the current unit & move onto the next unit.
            if (tempTarget == null)
            {
                tempTarget = unit;
                continue;
            }

            tempTarget = aimingOption switch
            {
                AimingOption.First => CheckIfFirstUnit(tempTarget, unit),
                AimingOption.Last => CheckIfLastUnit(tempTarget, unit),
                _ => CheckIfFirstUnit(tempTarget, unit),
            };
        }
        return tempTarget;
    }

    private GameObject CheckIfFirstUnit(GameObject tempTarget, GameObject unit)
    {
        // Get the movement components for the unit being checked and the current target.
        var unitMovement = unit.GetComponent<PointsBasedMovement>();
        var targetMovement = tempTarget.GetComponent<PointsBasedMovement>();

        // Check each unit against each other to find out which is moving towards the furthest point along the path.
        if (unitMovement.CurrentPointTarget > targetMovement.CurrentPointTarget)
        {
            return unit;
        }

        if (unitMovement.CurrentPointTarget < targetMovement.CurrentPointTarget)
        {
            return tempTarget;
        }

        if (unitMovement.GetDistanceToCurrentPointTarget() < targetMovement.GetDistanceToCurrentPointTarget())
        {
            return unit;
        }

        return tempTarget;
    }

    private GameObject CheckIfLastUnit(GameObject tempTarget, GameObject unit)
    {
        // Get the movement components for the unit being checked and the current target.
        var unitMovement = unit.GetComponent<PointsBasedMovement>();
        var targetMovement = tempTarget.GetComponent<PointsBasedMovement>();

        // Check each unit against each other to find out which is moving towards the furthest point along the path.
        if (unitMovement.CurrentPointTarget < targetMovement.CurrentPointTarget)
        {
            return unit;
        }

        if (unitMovement.CurrentPointTarget > targetMovement.CurrentPointTarget)
        {
            return tempTarget;
        }

        if (unitMovement.GetDistanceToCurrentPointTarget() > targetMovement.GetDistanceToCurrentPointTarget())
        {
            return unit;
        }

        return tempTarget;
    }

    IEnumerator FireProjectile()
    {
        while (true)
        {
            yield return new WaitUntil(() => target != null);
            InstantiateProjectile(target);
            yield return new WaitForSeconds(fireRate);
        }
    }

    private void InstantiateProjectile(GameObject unit)
    {
        projectile = Instantiate(projectilePrefab, transform.position, transform.rotation);
        projectile.GetComponent<ProjectileMovement>().Unit = unit;
    }
}