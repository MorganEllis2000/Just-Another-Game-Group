using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

        for (int i = 0; i < 5; i++) {

            float angleStep = SpreadAngle / NumberOfProjectiles;
            float aimingAngle = FirePoint.rotation.eulerAngles.z;
            float centeringOffset = (SpreadAngle / 2) - (angleStep / 2);

            float currentBulletAngle = angleStep * i;

            Quaternion rotation = Quaternion.Euler(new Vector3(0, 0, aimingAngle + currentBulletAngle - centeringOffset));
            GameObject bullet = Instantiate(ammo, FirePoint.transform.position, rotation);

            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.AddForce(bullet.transform.right * 15, ForceMode2D.Impulse);

            //ammo.transform.position = new Vector3(FirePoint.transform.position.x, (Random.Range(FirePoint.transform.position.y - range, FirePoint.transform.position.y + range)));
            //GameObject Bullet = Instantiate(ammo, ammo.transform.position, ammo.transform.rotation);
            //Bullet.GetComponent<Rigidbody2D>().AddForce(FirePoint.right * 15, ForceMode2D.Impulse);

            //GameObject Bullet = Instantiate(ammo, FirePoint.transform.position, FirePoint.transform.rotation);
            //Bullet.GetComponent<Rigidbody2D>().AddForce(FirePoint.right * 15, ForceMode2D.Impulse);

            //if (i == 0) {
            //    GameObject Bullet = Instantiate(ammo, FirePoint.position, FirePoint.rotation);
            //    Bullet.GetComponent<Rigidbody2D>().AddForce(FirePoint.right * 500 + new Vector3(0, 60f, 0), ForceMode2D.Force);
            //} else if (i == 1) {
            //    GameObject Bullet = Instantiate(ammo, FirePoint.position, FirePoint.rotation);
            //    Bullet.GetComponent<Rigidbody2D>().AddForce(FirePoint.right * 500 + new Vector3(0, 30f, 0), ForceMode2D.Force);
            //} else if (i == 2) {
            //    GameObject Bullet = Instantiate(ammo, FirePoint.position, FirePoint.rotation);
            //    Bullet.GetComponent<Rigidbody2D>().AddForce(FirePoint.right * 500 + new Vector3(0, 0f, 0), ForceMode2D.Force);
            //} else if (i == 3) {
            //    GameObject Bullet = Instantiate(ammo, FirePoint.position, FirePoint.rotation);
            //    Bullet.GetComponent<Rigidbody2D>().AddForce(FirePoint.right * 500 + new Vector3(0, -30f, 0), ForceMode2D.Force);
            //} else if (i == 4) {
            //    GameObject Bullet = Instantiate(ammo, FirePoint.position, FirePoint.rotation);
            //    Bullet.GetComponent<Rigidbody2D>().AddForce(FirePoint.right * 500 + new Vector3(0, -60f, 0), ForceMode2D.Force);
            //}
        }




        //Quaternion newRot = FirePoint.rotation;
        //for (int i = 0; i < 5; i++) {
        //    float addedOffset = (i - (6 / 2) * 4);

        //    newRot = Quaternion.Euler(FirePoint.transform.localEulerAngles.x, FirePoint.transform.localEulerAngles.y, FirePoint.transform.localEulerAngles.z + 30);

        //    float randomY = Random.Range(-1f, 1f);
        //    GameObject Bullet = Instantiate(ammo, new Vector3(FirePoint.position.x, FirePoint.position.y, FirePoint.position.z), newRot);
        //    Bullet.GetComponent<Rigidbody2D>().AddForce(FirePoint.right * 15, ForceMode2D.Impulse);
        //}

        //GameObject Bullet = Instantiate(ammo, FirePoint.position, FirePoint.rotation);
        //Bullet.GetComponent<Rigidbody2D>().AddForce(FirePoint.right * 15, ForceMode2D.Impulse);

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
