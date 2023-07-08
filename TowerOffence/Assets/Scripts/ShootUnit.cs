using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ShootUnit : MonoBehaviour
{
    [SerializeField] private float radius = 5f;
    [SerializeField] private float rotationSpeed = 500f;
    [SerializeField] private GameObject projectilePrefab;

    [SerializeField] private GameObject target;

    private GameObject projectile = null;

    // Update is called once per frame
    void FixedUpdate()
    { 
        GameObject unit = target;

        if (Vector3.Distance(transform.position, unit.transform.position) > radius)
        {
            return;
        }

        float angle = Mathf.Atan2(unit.transform.position.y - transform.position.y, unit.transform.position.x - transform.position.x) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.Euler(new Vector3(0, 0, angle - 90));
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime);

        if (projectile == null)
        {
            InstantiateProjectile(unit);
        }
    }

    private void InstantiateProjectile(GameObject unit)
    {
        projectile = Instantiate(projectilePrefab, transform.position, transform.rotation);
        projectile.GetComponent<ProjectileMovement>().Unit = unit;
    }
}
