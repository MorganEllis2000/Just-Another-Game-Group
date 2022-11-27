using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OxygenPickup : MonoBehaviour
{
    [SerializeField] private float AdditionalOxygen;

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Player")) {
            PlayerController.Instance.Oxygen += AdditionalOxygen;
            OxygenManager.Instance.IncreaseOxygen();
            Destroy(this.gameObject);
        }
    }
}
