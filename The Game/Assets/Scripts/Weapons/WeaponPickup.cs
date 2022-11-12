using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    [SerializeField] protected GameObject WeaponToEquip;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //private void OnTriggerEnter2D(Collider2D collision) {
    //    if (collision.gameObject.CompareTag("Playyer")) {
    //        if (Input.GetKey(KeyCode.F)) {
    //            WeaponManager.Instance.EquipOneHandedWeapon(WeaponToEquip);
    //        }
    //    }
    //}

    private void OnTriggerStay2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Player") && Input.GetKey(KeyCode.F)) {
            WeaponManager.Instance.EquipOneHandedWeapon(WeaponToEquip);
            this.gameObject.SetActive(false);
        }
    }
}
