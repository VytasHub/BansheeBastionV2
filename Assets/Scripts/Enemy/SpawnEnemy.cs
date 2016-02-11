using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpawnEnemy : MonoBehaviour {

    public List<GameObject> spawnSpots;
    public List<EnemyLevel> enemies;

    private EnemyLevel currentlevel;

    private bool enemySpawnBlock = true;


    // initializes the current level and switches to next one
    private void initLevel()
    {
        if (currentlevel == null)
        {
            currentlevel = enemies[0];
        } else
        {
            for (int i = 0; i < enemies.Count; i++)
            {
                if (enemies[i] == currentlevel && enemies[i + 1] != null)
                {
                    currentlevel = enemies[i + 1];
                }
            }
        }
    }

    // spawns the enemies
    private void spawnEnemies()
    {
        if (enemySpawnBlock)
        {
            enemySpawnBlock = false;

            bool spawned = false;
            int counter = 0;

            while(!spawned)
            {
                // spawn enemy and deduct the enemy counter to switch the level
                int spawnspot = Random.Range(0, spawnSpots.Count-1);

                EnemySpawnArea espArea = spawnSpots[spawnspot].GetComponentInChildren<EnemySpawnArea>();
                if (!espArea.busy)
                {
                    spawnEnemy(spawnSpots[spawnspot]);

                    spawned = true;
                }

                // exits from a loop in case all spawn places busy
                if (counter++ > 6)
                    break;
            }

            StartCoroutine(spawnWait(currentlevel.delay));
        }
        
    }

    private void spawnEnemy(GameObject spawnspot)
    {
        Vector2 position = spawnspot.transform.position;

        // randomly choose enemy
        int enemy = Random.Range(0, currentlevel.visualization.Count);

        // instaciate
        GameObject instance = Instantiate(currentlevel.visualization[enemy], position, Quaternion.identity) as GameObject;
        instance.transform.SetParent(gameObject.transform);
        MoveEnemy mv = instance.GetComponentInChildren<MoveEnemy>();
        mv.spawnArea = spawnspot;
    }

    private IEnumerator spawnWait(float sec)
    {
        yield return new WaitForSeconds(sec);
        enemySpawnBlock = true;
    }



    // Use this for initialization
    void Start () {
        initLevel();
	}
	
	// Update is called once per frame
	void Update () {
        spawnEnemies();

    }
}

[System.Serializable]
public class EnemyLevel
{
    public int enemies;
    public float delay;
    public int level;
    public List<GameObject> visualization;
}