using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemy;

    public IEnumerator SpawnEnemy() {
        Instantiate(enemy, this.transform.position, this.transform.rotation);
        yield return new WaitForSeconds(10);
    }
}
