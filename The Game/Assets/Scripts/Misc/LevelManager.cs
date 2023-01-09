// WRITTEN BY: MORGAN ELLIS

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Used to manage the tutorial level so that the exit only opens when all of the enemies within the level have been killed
/// </summary>
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

    // Update is called once per frame
    void Update()
    {
        if (NumberOfEnemies <= 0) {
            Roots.SetActive(false);
        }
    }
}
