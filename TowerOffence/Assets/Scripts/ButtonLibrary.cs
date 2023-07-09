using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonLibrary : MonoBehaviour
{
    public void Purchase(GameObject playerPrefab)
    {
        GameObject newUnit = Instantiate(playerPrefab, GameObject.FindWithTag("LevelPath").transform.GetChild(0));
        GameManager.Instance.Units.Add(newUnit);
    }
}
