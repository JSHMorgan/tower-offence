using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Statics
    public static GameManager Instance { get; private set; }

    // Instances
    [SerializeField] private int playerHealth = 100;
    [SerializeField] private int money = 1000;
    public int PlayerHealth { get => playerHealth; set => playerHealth = value; }
    public List<GameObject> Units { get; private set; } = new();
    public int Money { get => money; set => money = value; }

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
