////// WRITTEN BY: MORGAN ELLIS
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseRangeAttackCollision : MonoBehaviour
{
    [SerializeField] private float Damage;

    // If the player is inside the trigger on the close attack of the rock they take damage
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Player")) {
            PlayerController.Instance.TakeDamage(Damage);
        }
    }
}
