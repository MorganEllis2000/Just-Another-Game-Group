using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] protected float BulletSpeed;
    private Rigidbody2D rigidBody;
    [SerializeField] protected float LiveTime;

    private void Start() {
        rigidBody = GetComponent<Rigidbody2D>();
        //rigidBody.velocity = transform.forward * BulletSpeed;
    }

    private void Update() {
        this.transform.position += -transform.up * BulletSpeed * Time.deltaTime;
        LiveTime -= Time.deltaTime;
        if(LiveTime <= 0f) {
            Destroy(gameObject);
        }
    }
}
