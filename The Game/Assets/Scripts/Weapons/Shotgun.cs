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
        //InstatiatePistolAmmo(ammo.GetComponent<Bullet>());

        GameObject Bullet = Instantiate(ammo, FirePoint.position, FirePoint.rotation);
        Bullet.GetComponent<Rigidbody2D>().AddForce(-FirePoint.up * 15, ForceMode2D.Impulse);


        //Ray ray = Camera.main.ScreenPointToRay(GunRotate.Instance.CrossHair.transform.position);
        //GameObject Bullet = Instantiate(ammo, FirePoint.position, FirePoint.rotation);
        //Vector3 target = GunRotate.Instance.CrossHair.transform.position;
        //Vector2.MoveTowards(this.transform.position, target + new Vector3(100,100,0), 15 * Time.deltaTime);
        //Rigidbody2D rb = Bullet.GetComponent<Rigidbody2D>();
        //Vector2 direction = (ray.GetPoint(10000.0f) - Bullet.transform.position).normalized;
        //rb.AddForce(direction * 5, ForceMode2D.Impulse);

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
