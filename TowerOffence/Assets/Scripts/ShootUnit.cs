using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ShootUnit : MonoBehaviour
{
    [SerializeField] private float radius = 5f;
    [SerializeField] private float rotationSpeed = 500f;
    [SerializeField] private GameObject projectilePrefab;

    private GameObject projectile = null;

    // Update is called once per frame
    void FixedUpdate()
    { 
        GameObject target = FindFirstUnitInRange();

        if (target == null)
        {
            return;
        }

        float angle = Mathf.Atan2(target.transform.position.y - transform.position.y, target.transform.position.x - transform.position.x) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.Euler(new Vector3(0, 0, angle - 90));
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime);

        if (projectile == null)
        {
            InstantiateProjectile(target);
        }
    }

    private GameObject FindFirstUnitInRange()
    {
        GameObject target = null;

        GameObject[] units = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject unit in units)
        {
            //Debug.Log(unit);
            List<GameObject> targets = new();

            // Check if unit is visible to the main camera.
            if (!unit.GetComponent<SpriteRenderer>().isVisible)
            {
                continue;
            }

            // Check if unit has movement script.
            if (unit.GetComponent<PointsBasedMovement>() == null)
            {
                continue;
            }

            // Check if unit is within the range of the tower.
            if (Vector3.Distance(transform.position, unit.transform.position) > radius)
            {
                continue;
            }

            if (target == null)
            {
                target = unit;
                continue;
            }

            if (unit.GetComponent<PointsBasedMovement>().GetDistanceToCurrentPointTarget() < target.GetComponent<PointsBasedMovement>().GetDistanceToCurrentPointTarget())
            {
                target = unit;
            }
        }
        return target;
    }
    private void InstantiateProjectile(GameObject unit)
    {
        projectile = Instantiate(projectilePrefab, transform.position, transform.rotation);
        projectile.GetComponent<ProjectileMovement>().Unit = unit;
    }
}
