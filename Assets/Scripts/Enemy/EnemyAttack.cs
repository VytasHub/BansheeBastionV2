using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyAttack : MonoBehaviour {


    public List<GameObject> enemiesInRange;

    // Use this for initialization
    void Start () {
        enemiesInRange = new List<GameObject>();
    }

    // 1
    void OnEnemyDestroy(GameObject enemy)
    {
        enemiesInRange.Remove(enemy);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // 2
        if (other.gameObject.tag.Equals("Tower"))
        {
            enemiesInRange.Add(other.gameObject);
            TowerDestructionDelegate del =
                other.gameObject.GetComponent<TowerDestructionDelegate>();
            del.enemyDelegate += OnEnemyDestroy;
        }
    }
    // 3
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag.Equals("Tower"))
        {
            enemiesInRange.Remove(other.gameObject);
            TowerDestructionDelegate del =
                other.gameObject.GetComponent<TowerDestructionDelegate>();
            del.enemyDelegate -= OnEnemyDestroy;
        }
    }
}
