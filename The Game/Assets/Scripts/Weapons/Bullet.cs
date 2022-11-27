using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] protected float BulletSpeed;
    private Rigidbody2D rigidBody;
    [SerializeField] protected float LiveTime;
    [SerializeField] public Vector3 target;

    private void Start() {
        rigidBody = GetComponent<Rigidbody2D>();
        //rigidBody.velocity = transform.forward * BulletSpeed;
    }

    private void Update() {
        //Vector2.MoveTowards(this.transform.position, target, BulletSpeed * Time.deltaTime);
        //this.transform.position += -transform.forward * BulletSpeed * Time.deltaTime;
        //transform.Translate((transform.forward * BulletSpeed * Time.deltaTime));
        //this.GetComponent<Rigidbody2D>().AddForce(this.transform.forward * BulletSpeed, ForceMode2D.Impulse);
        LiveTime -= Time.deltaTime;
        if(LiveTime <= 0f) {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Enemy")) {
            collision.gameObject.GetComponent<Enemy>().Health -= 30.0f;
            Destroy(this.gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {
         if (collision.gameObject.CompareTag("Wall")) {
            Destroy(this.gameObject);
        }
    }
}
