using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ProjectileMovement : MonoBehaviour
{
    [SerializeField] private float speed = 5.0f;
    [SerializeField] private float unitDistance = 0.1f;

    private GameObject unit = null;
    public GameObject Unit 
    {
        private get
        {
            return unit;
        }
        set
        {
            unit = value;
            bool gotComponent = unit.TryGetComponent(out PointsBasedMovement component);
            speed = (gotComponent) ? component.Speed * 2.0f : 5.0f;
        }
    }

    private void FixedUpdate()
    {
        if (Unit == null)
        {
            return;
        }

        // Make the projectile move towards & face towards the unit it is attacking.
        transform.position = Vector2.MoveTowards(transform.position, Unit.transform.position, speed * Time.fixedDeltaTime);

        float angle = Mathf.Atan2(unit.transform.position.y - transform.position.y, unit.transform.position.x - transform.position.x) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.Euler(new Vector3(0, 0, angle - 90));
        transform.rotation = targetRotation;

        // Destroy the Unit and GameObject when within a certain range.
        if (Vector2.Distance(transform.position, Unit.transform.position) < unitDistance)
        {
            Destroy(gameObject);
        }
    }
}