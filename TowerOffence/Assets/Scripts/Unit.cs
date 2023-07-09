using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.UI;

public class Unit : MonoBehaviour
{
    [SerializeField] private Sprite sprite;
    [SerializeField] private int health = 5;
    [SerializeField] private float speed = 5;
    [SerializeField] private int cost = 0;

    public bool HasSpeedUp { get; set; }
    public bool HasHealthUp { get; set; }

    private void Awake()
    {
        GameManager.Instance.Units.Add(gameObject);
        Debug.Log(gameObject + " added to unit list.");
        if (sprite != null)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = sprite;
        }
    }

    public int Health
    { 
        get => health;
        set => health = value;
    }
    public float Speed 
    { 
        get => speed; 
        set => speed = value; 
    }

    public int Cost
    {
        get => cost;
        set => cost = value;
    }

    private void LateUpdate()
    {
        if (health <= 0)
        {
            StartCoroutine(HandleDestruction());
        }
    }

    internal void DealDamage(int damageValue)
    {
        Health -= damageValue;
    }

    internal float GetDistanceFromPoint(Vector3 point)
    {
        return Vector2.Distance(transform.position, point);
    }

    IEnumerator HandleDestruction()
    {
        yield return new WaitForEndOfFrame();
        GameManager.Instance.Units.Remove(gameObject);
        Destroy(gameObject);
    }
}