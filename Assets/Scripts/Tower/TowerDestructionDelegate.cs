using UnityEngine;
using System.Collections;

public class TowerDestructionDelegate : MonoBehaviour {

    public delegate void EnemyDelegate(GameObject enemy);
    public EnemyDelegate enemyDelegate;

    void OnDestroy()
    {
        if (enemyDelegate != null)
        {
            enemyDelegate(gameObject);
        }
    }
}
