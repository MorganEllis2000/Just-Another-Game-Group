using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get; private set; }

    public int NumberOfEnemies;
    public GameObject Roots;


    private void Awake() {

        if (Instance != null && Instance != this) {
            Destroy(gameObject);
        }

        Instance = this;
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (NumberOfEnemies <= 0) {
            Roots.SetActive(false);
        }
    }
}
