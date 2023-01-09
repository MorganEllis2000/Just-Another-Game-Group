// WRITTEN BY: MORGAN ELLIS

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This is the parent class from which all types of bullets in the game inherits from
/// </summary>
public class Bullet : MonoBehaviour
{
    [SerializeField] protected float BulletSpeed; // how fast the bullet travels
    private Rigidbody2D rigidBody;
    [SerializeField] protected float LiveTime; // time before it is destroyed
    [SerializeField] public Vector3 target;
    [SerializeField] private int Damage; // damage it will do to a target

    private void Start() {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    private void Update() {
        LiveTime -= Time.deltaTime;
        if(LiveTime <= 0f) {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Enemy")) {
            collision.gameObject.GetComponent<Enemy>().Health -= Damage;
            Destroy(this.gameObject);
        }

        if (collision.gameObject.CompareTag("Wall")) {
            Destroy(this.gameObject);
        }
    }
}
