using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Chaos : Unit
{
    [SerializeField] private GameObject playerPrefab;
    private void OnDestroy()
    {
        Debug.Log("Chaos Killed");
        Transform pos = transform;
        GameObject newUnit = Instantiate(playerPrefab, GameObject.Find("StartPoint").transform);
        GameManager.Instance.Units.Add(newUnit);
    }
}
