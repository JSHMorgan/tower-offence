using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.VisualScripting;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private float pointDistance = 0.1f;

    private Transform[] points;
    private int counter = 0;

    private void Start()
    {
        GameObject levelPathParent = GameObject.FindWithTag("LevelPath");
        points = GetAllChildrenOfGameObject(levelPathParent);
    }

    private void FixedUpdate()
    {
        if (counter == points.Length)
        {
            Destroy(gameObject);
            return;
        }

        transform.position = Vector2.MoveTowards(transform.position, points[counter].transform.position, speed * Time.fixedDeltaTime);

        if (Vector2.Distance(transform.position, points[counter].position) < pointDistance)
        {
            counter++;
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
}