using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : Weapon
{
    [SerializeField] private GameObject ammo;

    private void Start() {
        CurrentAmmo = MaxAmmo;
        Reloading = false;
        CanShoot = true;
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
        } else if (CurrentAmmo <= 0) {
            Debug.Log("Reloading...");
            StartCoroutine("Reload");
        }
    }

    IEnumerator FireRate() {
        CanShoot = false;

        for(int i = 0; i < 5; i++) {
            float randomY = Random.Range(-1f, 1f);
            GameObject Bullet = Instantiate(ammo, new Vector3(FirePoint.position.x, FirePoint.position.y + randomY, FirePoint.position.z), FirePoint.rotation);
            Bullet.GetComponent<Rigidbody2D>().AddForce(FirePoint.right * 15, ForceMode2D.Impulse);
        }
      
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
}
