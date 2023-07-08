using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.VisualScripting;
using UnityEngine;

public class PointsBasedMovement : MonoBehaviour
{
    [SerializeField] private float speed = 5.0f;
    [SerializeField] private float pointDistance = 0.1f;

    private Transform[] points;

    public float Speed
    {
        get => speed;
        private set => speed = value;
    }
    public int CurrentPointTarget { get; private set; }

    private void Start()
    {
        GameObject levelPathParent = GameObject.FindWithTag("LevelPath");
        points = GetAllChildrenOfGameObject(levelPathParent);
    }

    private void FixedUpdate()
    {
        if (CurrentPointTarget == points.Length)
        {
            Destroy(gameObject);
            return;
        }

        transform.position = Vector2.MoveTowards(transform.position, points[CurrentPointTarget].transform.position, speed * Time.fixedDeltaTime);

        if (GetDistanceToCurrentPointTarget() < pointDistance)
        {
            CurrentPointTarget++;
        }
    }

    private Transform[] GetAllChildrenOfGameObject(GameObject gameObject)
    {
        List<Transform> children = new();

        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            Transform child = gameObject.transform.GetChild(i);
            children.Add(child.transform);
        }
        return children.ToArray();
    }

    public float GetDistanceToCurrentPointTarget()
    {
        return Vector2.Distance(transform.position, points[CurrentPointTarget].position);
    }
}