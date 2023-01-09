// WRITTEN BY: KISHEN
// EDITED BY: MORGAN ELLIS

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Used to track the health of the player
/// </summary>
public class HealthBar : MonoBehaviour {
    public Slider slider;

    private void Start() {
        slider.maxValue = PlayerController.Instance.Health;
        slider.value = PlayerController.Instance.Health;
    }

    private void Update() {
        slider.value = PlayerController.Instance.Health;
    }
}
