using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class Pistol : Weapon {

    [SerializeField] private GameObject ammo;

    private void Start() {
        CurrentAmmo = MaxAmmo;
        Reloading = false;
        CanShoot = true;
    }

    private void Update() {
        if(CurrentAmmo > MaxAmmo) {
            CurrentAmmo = MaxAmmo;
        }

        if (Input.GetKeyDown(KeyCode.Mouse0) && CurrentAmmo > 0 && CurrentAmmo <= MaxAmmo && CanShoot == true && Reloading == false && PlayerController.Instance.isDashing == false) {
            Debug.Log("Shooting");
            Debug.Log("Current Ammo " + CurrentAmmo);
            CurrentAmmo--;
            StartCoroutine("FireRate");      
        } else if (CurrentAmmo <= 0) {
            Debug.Log("Reloading...");
            StartCoroutine("Reload");
        }
    }

    IEnumerator FireRate() {
        CanShoot = false;
        InstatiateAmmo(ammo);
        yield return new WaitForSeconds(fireRate);
        CanShoot = true;
    }

    IEnumerator Reload() {
        CurrentAmmo = MaxAmmo;
        Reloading = true;
        CanShoot = false;
        this.gameObject.GetComponent<SpriteRenderer>().color = Color.red;
        yield return new WaitForSeconds(ReloadTime);
        this.gameObject.GetComponent<SpriteRenderer>().color = Color.white;
        Reloading = false;
        CanShoot = true;
    }

    //private void OnTriggerEnter2D(Collider2D collision) {
    //    if (collision.gameObject.CompareTag("Playyer")){
    //        if (Input.GetKey(KeyCode.F)) {
    //            WeaponManager.Instance.EquipOneHandedWeapon(this.gameObject);
    //        }
    //    }
    //}
}
