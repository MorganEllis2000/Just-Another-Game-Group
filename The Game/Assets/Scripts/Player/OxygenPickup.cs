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
            this.GetComponent<AudioSource>().Play();
            this.GetComponent<BoxCollider2D>().enabled = false;
            this.GetComponent<SpriteRenderer>().enabled = false;
            Destroy(this.gameObject, 3);
        }
    }
}
