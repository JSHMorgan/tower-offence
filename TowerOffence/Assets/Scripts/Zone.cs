using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Zone : MonoBehaviour
{
    [SerializeField] protected float lifetime;
    [SerializeField] protected float radius;

    private void Start()
    {
        StartCoroutine(DestroyAfterLifetime());
    }

    protected IEnumerator DestroyAfterLifetime()
    {
        yield return new WaitForSeconds(lifetime);
        Destroy(gameObject);
    }
}
