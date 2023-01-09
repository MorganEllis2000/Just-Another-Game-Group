// WRITTEN BY: MORGAN ELLIS

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// If the player collides with an oxygen tank they will can additional oxygen based on the float value up to 
/// the value MaxOxygen which is assigned on the player
/// </summary>
public class OxygenPickup : MonoBehaviour
{
    [SerializeField] private float AdditionalOxygen;

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Player")) {
            PlayerController.Instance.Oxygen += AdditionalOxygen;
            OxygenManager.Instance.IncreaseOxygen();
            this.GetComponent<AudioSource>().Play();
            this.GetComponent<BoxCollider2D>().enabled = false;
            this.GetComponent<SpriteRenderer>().enabled = false;
            Destroy(this.gameObject, 3);
        }
    }
}
