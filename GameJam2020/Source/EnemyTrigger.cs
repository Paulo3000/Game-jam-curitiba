using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTrigger : MonoBehaviour
{
    public Enemy[] enemies;

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.tag == "Player")
        {
            foreach (Enemy enemy in enemies)
            {
                enemy.WakeUp();
            }
        }
    }
}
