using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Statics
    public static GameManager Instance { get; private set; }

    // Instances
    [SerializeField] private int health = 100;
    public int Health { get => health; set => health = value; }
    public List<GameObject> Units { get; private set; } = new();

    private void Awake()
    {
        // If there is an instance, and it's me, return.
        if (Instance == this)
        {
            return;
        }

        // If there is no instance, set this to the instance.
        if (Instance == null)
        {
            Instance = this;
            return;
        }

        // Otherwise, destroy this instance.
        Destroy(this);
    }
}
