using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    [SerializeField] protected GameObject WeaponToEquip;
    [SerializeField] protected GameObject PickupText;

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
        PickupText.SetActive(true);
        if (collision.gameObject.CompareTag("Player")){
            if(Input.GetKey(KeyCode.F)) {
                WeaponManager.Instance.EquipOneHandedWeapon(WeaponToEquip);
                WeaponManager.Instance.Weapons.Add(WeaponToEquip);
                this.gameObject.SetActive(false);
                PickupText.SetActive(false);
            }

        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Player")) {
            PickupText.SetActive(false);
        }
    }
}
