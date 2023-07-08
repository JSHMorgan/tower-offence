using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public List<GameObject> Units { get; private set; } = new();

    private void Awake()
    {
        // If there is an instance, and it's not me, delete myself.

        if (Instance == this)
        {
            return;
        }

        if (Instance == null)
        {
            Instance = this;
            return;
        }

        Destroy(this);
    }

    private void Start()
    {
        foreach (var unit in GameObject.FindGameObjectsWithTag("Player"))
        {
            Units.Add(unit);
        }
    }
}
