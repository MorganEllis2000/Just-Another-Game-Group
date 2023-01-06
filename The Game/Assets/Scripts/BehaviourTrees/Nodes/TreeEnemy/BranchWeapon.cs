using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;

public class BranchWeapon : MonoBehaviour
{
    Vector3 PlayerLastKnownPos;
    public Rigidbody2D Rigidbody;
    [SerializeField] private float ThrowSpeed;
    private Vector3 TargetPos;
    private void Start() {
        //Vector2 MoveDirection = (PlayerController.Instance.gameObject.transform.position - transform.position).normalized * ThrowSpeed;
        //Rigidbody.velocity = new Vector2(MoveDirection.x, MoveDirection.y);
        TargetPos = PlayerController.Instance.transform.position;
        TargetPos = TargetPos + (-transform.forward * 20f);
        Destroy(gameObject, 1.5f);
    }

    private void Update() {
        transform.position = Vector2.MoveTowards(transform.position, TargetPos, ThrowSpeed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Player")) {
            PlayerController.Instance.TakeDamage(30f);
            Destroy(this.gameObject);
        }
    }
}
