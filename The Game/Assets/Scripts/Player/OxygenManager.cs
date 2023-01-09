// WRITTEN BY: MORGAN ELLIS

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This script is responsible for managing the players oxygen
/// It decreases at a rate of 1 but pauses if the player is engaged in dialogue
/// </summary>
public class OxygenManager : MonoBehaviour
{
    public static OxygenManager Instance { get; private set; }

    public Slider OxygenBar;

    public float RepeatDelay = 1.0f;

    private void Awake() {

        if (Instance != null && Instance != this) {
            Destroy(gameObject);
        }

        Instance = this;
    }

    void Start()
    {
        OxygenBar.maxValue = PlayerController.Instance.MaxOxygen;
        OxygenBar.value = PlayerController.Instance.Oxygen;
        //InvokeRepeating("DecreaseOxygen", RepeatDelay, 1.0f);
        StartCoroutine(DecreaseOxygenCoroutine());
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerController.Instance.IsTalking) {
            RepeatDelay = 10000f;
        } else {
            RepeatDelay = 1f;
        }
    }

    public void DecreaseOxygen() {
        PlayerController.Instance.Oxygen -= 1;
        OxygenBar.value -= 1;
        
    }

    public void IncreaseOxygen() {
        OxygenBar.value = PlayerController.Instance.Oxygen;
    }

    public IEnumerator DecreaseOxygenCoroutine() {
        while (true) {
            if (PlayerController.Instance.IsTalking == false) {
                DecreaseOxygen();
                yield return new WaitForSeconds(1);
            } else {
                yield return new WaitForSeconds(1);
            }
        }
    }
}
