using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ButtonLibrary : MonoBehaviour
{
    [SerializeField] private GameObject currencyText;

    private void Start()
    {
        currencyText.GetComponent<TMP_Text>().text = $"Money: £{GameManager.Instance.Money}";
    }
    public void Purchase(GameObject playerPrefab)
    {
        if ((GameManager.Instance.Money - playerPrefab.GetComponent<Unit>().Cost) >= 0)
        {
            GameManager.Instance.Money -= playerPrefab.GetComponent<Unit>().Cost;
            currencyText.GetComponent<TMP_Text>().text = $"Money: £{GameManager.Instance.Money}";
            GameObject newUnit = Instantiate(playerPrefab, GameObject.FindWithTag("LevelPath").transform.GetChild(0));
            GameManager.Instance.Units.Add(newUnit);
        }
    }
}
