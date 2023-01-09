// WRITTEN BY: MORGAN ELLIS

using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Pistol : Weapon {

    [SerializeField] private GameObject ammo;

    private void Start() {
        CurrentAmmo = MaxAmmo;
        Reloading = false;
        CanShoot = true;
        source = this.GetComponent<AudioSource>();
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
        } else if (CurrentAmmo <= 0 && Reloading == false) {
            Debug.Log("Reloading...");
            StartCoroutine(Reload());
        }

        if(Input.GetKeyDown(KeyCode.R) && CurrentAmmo < MaxAmmo && CurrentAmmo != 0 && Reloading == false) {
            StartCoroutine(Reload());
        }
    }

    /// <summary>
    /// This shoots the bullets out the gun but also is the fire rate of the gun
    /// </summary>
    /// <returns></returns>
    IEnumerator FireRate() {
        CanShoot = false;
        source.clip = ShootingSound;
        source.Play();

        GameObject Bullet = Instantiate(ammo, FirePoint.position, FirePoint.rotation);
        Bullet.GetComponent<Rigidbody2D>().AddForce(FirePoint.right * 15, ForceMode2D.Impulse);

        yield return new WaitForSeconds(fireRate);
        CanShoot = true;
    }

    IEnumerator Reload() {   
        Reloading = true;
        CanShoot = false;
        yield return new WaitForSeconds(1.0f);
        source.clip = ReloadingSound;
        source.Play();
        this.gameObject.GetComponent<SpriteRenderer>().color = Color.red;
        yield return new WaitForSeconds(ReloadTime - 1.0f);
        this.gameObject.GetComponent<SpriteRenderer>().color = Color.white;
        CurrentAmmo = MaxAmmo;
        Reloading = false;
        CanShoot = true;
    }
}
