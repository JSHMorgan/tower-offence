using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.UI;

public class Unit : MonoBehaviour
{
    [SerializeField] private Sprite sprite;
    [SerializeField] private int health = 5;

    private void Awake()
    {
        if (sprite != null)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = sprite;
        }
        GameManager.Instance.Units.Add(gameObject);
    }

    public int Health
    { 
        get => health;
        set => health = value;
    }

    private void LateUpdate()
    {
        if (health == 0)
        {
            Destroy(gameObject);
            return;
        }
    }
}