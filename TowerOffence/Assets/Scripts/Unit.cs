using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    [SerializeField] private int health = 5;

    public int Health 
    { 
        get => health;
        set => health = value;
    }

    private void Update()
    {
        if (health == 0)
        {
            Destroy(gameObject);
            return;
        }
    }
}
