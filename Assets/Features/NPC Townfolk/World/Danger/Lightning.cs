using System.Collections;
using UnityEngine;

public class Lightning : MonoBehaviour, IDanger
{
    private void Start()
    {
        StartCoroutine(DestroyAfterTime());
    }

    private IEnumerator DestroyAfterTime ()
    {
        yield return new WaitForSeconds(5.0f);
        Destroy(gameObject);
    }
}
