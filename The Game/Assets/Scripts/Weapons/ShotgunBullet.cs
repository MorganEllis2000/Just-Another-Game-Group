// WRITTEN BY: MORGAN ELLIS

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunBullet : MonoBehaviour {
    [SerializeField] protected float BulletSpeed;
    private Rigidbody2D rigidBody;
    [SerializeField] protected float LiveTime;
    [SerializeField] public Vector3 target;
    [SerializeField] private float Damage;

    private void Start() {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    private void Update() {
        LiveTime -= Time.deltaTime;
        if (LiveTime <= 0f) {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Because the boss also uses a shotgun two different types of shotgun bullets were created with different tags else 
    /// the bullet would collide with the boss itself meaning he was effectively shooting himself
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Enemy") && collision.gameObject.name != "InfectedAstronaut") {
            collision.gameObject.GetComponent<Enemy>().Health -= Damage;
            Destroy(this.gameObject);
        }

        if (collision.gameObject.CompareTag("Wall")) {
            Destroy(this.gameObject);
        }

        if (collision.gameObject.CompareTag("Player") && this.gameObject.CompareTag("EnemyBullet")) {
            PlayerController.Instance.TakeDamage(Damage);
            Destroy(this.gameObject);
        }

        if (collision.gameObject.CompareTag("Enemy") && this.gameObject.CompareTag("Bullet") && collision.gameObject.name == "InfectedAstronaut") {
            collision.gameObject.GetComponent<Enemy>().Health -= Damage;
            Destroy(this.gameObject);
        }
    }
}
