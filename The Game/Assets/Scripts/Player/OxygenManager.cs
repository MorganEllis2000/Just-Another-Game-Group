using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OxygenManager : MonoBehaviour
{
    public static OxygenManager Instance { get; private set; }


    public Slider OxygenBar;

    private void Awake() {

        if (Instance != null && Instance != this) {
            Destroy(gameObject);
        }

        Instance = this;
    }

    void Start()
    {
        OxygenBar.maxValue = PlayerController.Instance.MaxOxygen;
        OxygenBar.value = PlayerController.Instance.MaxOxygen;
        InvokeRepeating("DecreaseOxygen", 1.0f, 1.0f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DecreaseOxygen() {
        PlayerController.Instance.Oxygen -= 1;
        OxygenBar.value -= 1;
        
    }

    public void IncreaseOxygen() {
        OxygenBar.value = PlayerController.Instance.Oxygen;
    }
}
