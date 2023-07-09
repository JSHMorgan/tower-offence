using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ProjectileMovement : MonoBehaviour
{
    [Tooltip("Speed is based upon the speed of the projectile plus the speed of the unit it's aiming for.")]
    [SerializeField] private float projectileSpeed = 2.0f;
    [SerializeField] private float acceleration = 1.0f;
    [SerializeField] private float unitDistance = 0.1f;

    public int Damage { get; set; }

    public GameObject Unit { get; set; }

    private void FixedUpdate()
    {
        if (Unit == null)
        {
            Destroy(gameObject);
            return;
        }

        // Get the speed of the projectile.
        projectileSpeed += acceleration;

        // Make the projectile move towards the unit it's attacking.
        transform.position = Vector2.MoveTowards(transform.position, Unit.transform.position, projectileSpeed * Time.fixedDeltaTime);

        // Make the projectile face the unit it's moving towards.
        float angle = Mathf.Atan2(Unit.transform.position.y - transform.position.y, Unit.transform.position.x - transform.position.x) * Mathf.Rad2Deg;
        float offset = -90;
        Quaternion targetRotation = Quaternion.Euler(new Vector3(0, 0, angle + offset));
        transform.rotation = targetRotation;

        // Destroy the Projectile when within a certain distance of the unit.
        if (Unit.GetComponent<Unit>().GetDistanceFromPoint(transform.position) < unitDistance)
        {
            Destroy(gameObject);
            Unit.GetComponent<Unit>().Health -= Damage;
        }
    }
}