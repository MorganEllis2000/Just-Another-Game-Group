using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;

public class BranchWeapon : MonoBehaviour
{
    Vector3 PlayerLastKnownPos;
    public Rigidbody2D Rigidbody;
    private void Start() {
        Vector2 MoveDirection = (PlayerController.Instance.gameObject.transform.position - transform.position).normalized * 10;
        Rigidbody.velocity = new Vector2(MoveDirection.x, MoveDirection.y);
        Destroy(gameObject, 3f);
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Player")) {
            Destroy(this.gameObject);
        }
    }
}
