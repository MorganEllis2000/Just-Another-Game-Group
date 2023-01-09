// WRITTEN BY: MORGAN ELLIS

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootsWeapon : MonoBehaviour
{
    [SerializeField] private float Damage;
    private void OnTriggerEnter2D(Collider2D collision) {
        PlayerController.Instance.TakeDamage(Damage);
        this.gameObject.GetComponent<RootsWeapon>().enabled = false;
    }
}
