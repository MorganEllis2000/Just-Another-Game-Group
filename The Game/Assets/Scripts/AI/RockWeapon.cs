// WRITTEN BY: MORGAN ELLIS

using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;

/// <summary>
/// Attacked to the rock weapon prefab, this is what is thrown by the rock when
/// it performs a long range attack
/// </summary>
public class RockWeapon : MonoBehaviour {

    Vector3 PlayerLastKnownPos; 
    public Rigidbody2D Rigidbody;
    [SerializeField] private float ThrowSpeed;
    private Vector3 TargetPos;
    [SerializeField] private int Damage;

    private void Start() {
        TargetPos = PlayerController.Instance.transform.position; // Stores the plays last know position when the rock is instantiated 
        TargetPos = TargetPos + (-transform.forward * 20f);
        Destroy(gameObject, 1.5f);
    }

    /// <summary>
    /// Move the rock to the players last know position
    /// </summary>
    private void Update() {
        transform.position = Vector2.MoveTowards(transform.position, TargetPos, ThrowSpeed * Time.deltaTime);
    }

    // If the rock collides with the player damage the player else if it collides with a wall
    // destroy itself
    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Player")) {
            PlayerController.Instance.TakeDamage(Damage);
            Destroy(this.gameObject);
        }

        if (collision.gameObject.CompareTag("Wall")) {
            Destroy(this.gameObject);
        }
    }
}