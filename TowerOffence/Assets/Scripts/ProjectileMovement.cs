using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ProjectileMovement : MonoBehaviour
{
    [Tooltip("Speed is based upon the speed of the a")]
    [SerializeField] private float speed = 2.0f;
    [SerializeField] private float unitDistance = 0.1f;
    public GameObject Unit { get; set; }

    private void FixedUpdate()
    {
        if (Unit == null)
        {
            Destroy(gameObject);
            return;
        }

        // Get the speed of the projectile.
        speed = Unit.GetComponent<PointsBasedMovement>().Speed * speed;

        // Make the projectile move towards & face towards the unit it is attacking.
        transform.position = Vector2.MoveTowards(transform.position, Unit.transform.position, speed * Time.fixedDeltaTime);

        float angle = Mathf.Atan2(Unit.transform.position.y - transform.position.y, Unit.transform.position.x - transform.position.x) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.Euler(new Vector3(0, 0, angle - 90));
        transform.rotation = targetRotation;

        // Destroy the Unit and GameObject when within a certain range.
        if (Vector2.Distance(transform.position, Unit.transform.position) < unitDistance)
        {
            Destroy(gameObject);
            Unit.GetComponent<Unit>().Health--;
        }
    }
}