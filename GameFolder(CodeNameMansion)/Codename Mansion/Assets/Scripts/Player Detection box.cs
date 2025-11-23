using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] private List<Enemy_Movement> enemies = new List<Enemy_Movement>();
    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Enemy"))
        {
            enemies.Add(other.GetComponent<Enemy_Movement>());
        }

        if(other.CompareTag("Player"))
        {
            foreach(Enemy_Movement enemy in enemies)
            {
                enemy.chasing = true;
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            foreach (Enemy_Movement enemy in enemies)
            {
                enemy.chasing = true;
            }
        }
    }
    void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            foreach(Enemy_Movement enemy in enemies)
            {
                enemy.chasing = false;
            }
        }
    }
}
