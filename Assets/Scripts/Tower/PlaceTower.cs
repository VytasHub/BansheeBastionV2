using UnityEngine;
using System.Collections;

public class PlaceTower : MonoBehaviour {

    public GameObject towerPrefab;
    private GameObject tower;



    private bool canPlaceTower()
    {
        return tower == null;
    }

    private bool canUpgradeMonster()
    {
        if (tower != null)
        {
            TowerData towerData = tower.GetComponent<TowerData>();
            TowerLevel nextLevel = towerData.getNextLevel();
            if (nextLevel != null)
            {
                return true;
            }
        }
        return false;
    }

    void OnMouseUp()
    {
        if (canPlaceTower())
        {
            tower = (GameObject) Instantiate(towerPrefab, transform.position, Quaternion.identity);
            
            AudioSource audioSource = gameObject.GetComponent<AudioSource>();
            audioSource.PlayOneShot(audioSource.clip);

            // TODO: Deduct gold
        }
        else if (canUpgradeMonster())
        {
            tower.GetComponent<TowerData>().increaseLevel();
            AudioSource audioSource = gameObject.GetComponent<AudioSource>();
            audioSource.PlayOneShot(audioSource.clip);
            // TODO: Deduct gold
        }

    }

}
