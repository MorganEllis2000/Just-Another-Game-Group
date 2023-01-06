using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    public int NumberOfEnemies;
    [SerializeField] GameObject[] doors;
    [SerializeField] private GameObject enemy;



    void Start()
    {
            
    }

    void Update()
    {
        if(NumberOfEnemies == 0) {
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

            for (int j = 0; j < NumberOfEnemies; j++) {
                SpawnEnemy();
            }

            WFCExample.Instance.currentRoom = this;
        }
    }
    public float time = 0;
    public void SpawnEnemy() {
        
        time += Time.deltaTime;
        if(NumberOfEnemies > 0) {
            if (time < 10f) {
                GameObject Enemy = Instantiate(enemy, this.transform.position, this.transform.rotation);
                Enemy.SetActive(true);
                NumberOfEnemies -= 1;
                time = 0;
            }
        }              
    }
}
