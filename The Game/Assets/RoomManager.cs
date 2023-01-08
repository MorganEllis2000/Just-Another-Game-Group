using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    public int NumberOfEnemiesToSpawn;
    public int NumberOfEnemiesLeft;
    [SerializeField] GameObject[] doors;
    [SerializeField] private GameObject enemy;
    private bool CanSpawn = true;



    void Start()
    {
        NumberOfEnemiesLeft = NumberOfEnemiesToSpawn;
    }

    void Update()
    {
        if(NumberOfEnemiesToSpawn == 0) {
            CanSpawn = false;
        }

        if(NumberOfEnemiesLeft <= 0) {
            for (int i = 0; i < doors.Length; i++) {
                doors[i].SetActive(false);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Player")) {
            for(int i = 0; i < doors.Length; i++) {
                doors[i].SetActive(true);
            }
            StartCoroutine(SpawnEnemies());
            //for (int j = 0; j < NumberOfEnemies; j++) {
            //    SpawnEnemy();
            //}

            WFCExample.Instance.currentRoom = this;
            this.gameObject.GetComponent<BoxCollider2D>().enabled = false;
        }
    }

    public IEnumerator SpawnEnemies() {
        while (CanSpawn) {
            GameObject Enemy = Instantiate(enemy, this.transform.position, this.transform.rotation);
            Enemy.SetActive(true);
            NumberOfEnemiesToSpawn -= 1;
            yield return new WaitForSeconds(6);
        }
    }
}
