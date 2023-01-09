// WRITTEN BY: MORGAN ELLIS

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// This script is responsible for controlling the shotgun that the player uses
/// </summary>
public class Shotgun : Weapon
{
    [SerializeField] private GameObject ammo;
    private float SpreadAngle = 20f;
    private int NumberOfProjectiles = 6;
    [SerializeField] private float range;

    private void Start() {
        CurrentAmmo = MaxAmmo;
        Reloading = false;
        CanShoot = true;
        source = this.GetComponent<AudioSource>();
    }

    private void Update() {
        if (CurrentAmmo > MaxAmmo) {
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

        if (Input.GetKeyDown(KeyCode.R) && CurrentAmmo < MaxAmmo && CurrentAmmo != 0 && Reloading == false) {
            StartCoroutine(Reload());
        }
    }

    IEnumerator FireRate() {
        CanShoot = false;

        source.clip = ShootingSound;
        source.Play();
        yield return new WaitForSeconds(0.25f);
        for (int i = 0; i < 5; i++) {

            float angleStep = SpreadAngle / NumberOfProjectiles;
            float aimingAngle = FirePoint.rotation.eulerAngles.z;
            float centeringOffset = (SpreadAngle / 2) - (angleStep / 2);

            float currentBulletAngle = angleStep * i;

            Quaternion rotation = Quaternion.Euler(new Vector3(0, 0, aimingAngle + currentBulletAngle - centeringOffset));
            GameObject bullet = Instantiate(ammo, FirePoint.transform.position, rotation);

            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.AddForce(bullet.transform.right * 15, ForceMode2D.Impulse);
        }

        yield return new WaitForSeconds(fireRate - 0.25f);
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
