using UnityEngine;

public class DebugPanel : MonoBehaviour
{
    [SerializeField] private Transform player;
    void Update()
    {
        gameObject.transform.rotation = Quaternion.FromToRotation(player.position, gameObject.transform.position);
    }
}
