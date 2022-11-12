using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunRotate : MonoBehaviour
{
    public PlayerController controller;

    private void FixedUpdate() {
        Vector3 diffrence = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        diffrence.Normalize();

        float rotaionZ = Mathf.Atan2(diffrence.y, diffrence.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotaionZ);

        if(transform.eulerAngles.z >= 0 && transform.eulerAngles.z <= 89) {
            controller.SetGunDirection(GunDirection.NE);
            controller.SetPlayerDirection(PlayerDirection.BACK);
            if (WeaponManager.Instance.CurrentlyEquippedWeapon != null) {
                WeaponManager.Instance.CurrentlyEquippedWeapon.gameObject.GetComponent<SpriteRenderer>().flipY = true;
            }
        }
        if (transform.eulerAngles.z >= 180 && transform.eulerAngles.z <= 269) {
            controller.SetGunDirection(GunDirection.SW);
            controller.SetPlayerDirection(PlayerDirection.FRONT);
            if (WeaponManager.Instance.CurrentlyEquippedWeapon != null) {
                WeaponManager.Instance.CurrentlyEquippedWeapon.gameObject.GetComponent<SpriteRenderer>().flipY = false;
            }
        }
        if (transform.eulerAngles.z >= 89 && transform.eulerAngles.z <= 179) {
            controller.SetGunDirection(GunDirection.NW);
            controller.SetPlayerDirection(PlayerDirection.BACK);
            if (WeaponManager.Instance.CurrentlyEquippedWeapon != null) {
                WeaponManager.Instance.CurrentlyEquippedWeapon.gameObject.GetComponent<SpriteRenderer>().flipY = false;
            }

        }
        if (transform.eulerAngles.z >= 269 && transform.eulerAngles.z <= 360) {
            controller.SetGunDirection(GunDirection.SE);
            controller.SetPlayerDirection(PlayerDirection.FRONT);
            if (WeaponManager.Instance.CurrentlyEquippedWeapon != null) {
                WeaponManager.Instance.CurrentlyEquippedWeapon.gameObject.GetComponent<SpriteRenderer>().flipY = true;
            }
        }
    }
}
