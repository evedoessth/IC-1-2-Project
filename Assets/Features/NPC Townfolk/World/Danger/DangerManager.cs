using UnityEngine;

public class DangerManager : MonoBehaviour
{
    [SerializeField] private GameObject lightningPrefab;
    [SerializeField] private bool controlMode;

    public void CreateDanger(GameObject player)
    {
        float radius = 12.0f;
        Vector3 randomPos = player.transform.position + (Random.onUnitSphere * radius);
        if (controlMode)
        {
            randomPos = player.transform.position + new Vector3(0.0f, 0.0f, radius);
        }
        randomPos.y = 0.0f;
        GameObject lightning = Instantiate(lightningPrefab, randomPos, Quaternion.identity);
    }
}
