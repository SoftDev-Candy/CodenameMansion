using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PlayerDeathRespawn>().CurrentCheckPoint = transform;
        }
    }
}
