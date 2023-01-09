using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunRotate : MonoBehaviour
{
    public static GunRotate Instance { get; private set; }

    public PlayerController controller;
    public GameObject CrossHair;


    private void Awake() {

        if (Instance != null && Instance != this) {
            Destroy(gameObject);
        }

        Instance = this;
    }

    private void Update() {
        //MoveCrosshair();
    }

    private void FixedUpdate() {
        if (PlayerController.Instance.IsTalking == false) {
            Vector3 diffrence = Camera.main.ScreenToWorldPoint(Input.mousePosition) - CrossHair.transform.position;
            if (diffrence.magnitude > 0.0f) {
                diffrence.Normalize();
                diffrence *= 5f;
                //diffrence.y += 0.05f;
                CrossHair.transform.localPosition = diffrence;
            }
            //diffrence.Normalize();

            float rotaionZ = Mathf.Atan2(diffrence.y, diffrence.x) * Mathf.Rad2Deg;

            if (WeaponManager.Instance.CurrentlyEquippedWeapon != null) {
                WeaponManager.Instance.CurrentlyEquippedWeapon.transform.rotation = Quaternion.Euler(0f, 0f, rotaionZ);

                if (WeaponManager.Instance.CurrentlyEquippedWeapon.transform.eulerAngles.z >= 0 && WeaponManager.Instance.CurrentlyEquippedWeapon.transform.eulerAngles.z <= 89) {
                    
                    controller.SetGunDirection(GunDirection.NE);
                    controller.SetPlayerDirection(PlayerDirection.BACK);

                    if (WeaponManager.Instance.CurrentlyEquippedWeapon.name == "Pistol") {
                        this.transform.localPosition = new Vector3(0.64f, 0.09f, 0);
                        WeaponManager.Instance.CurrentlyEquippedWeapon.gameObject.GetComponent<SpriteRenderer>().flipY = true;
                    } else if (WeaponManager.Instance.CurrentlyEquippedWeapon.name == "Shotgun") {
                        this.transform.localPosition = new Vector3(0.15f, -0.029f, 0);
                        WeaponManager.Instance.CurrentlyEquippedWeapon.gameObject.GetComponent<SpriteRenderer>().flipY = false;
                    }
                }
                if (WeaponManager.Instance.CurrentlyEquippedWeapon.transform.eulerAngles.z >= 180 && WeaponManager.Instance.CurrentlyEquippedWeapon.transform.eulerAngles.z <= 269) {
                    
                    controller.SetGunDirection(GunDirection.SW);
                    controller.SetPlayerDirection(PlayerDirection.FRONT);

                    if (WeaponManager.Instance.CurrentlyEquippedWeapon.name == "Pistol") {
                        this.transform.localPosition = new Vector3(-0.91f, -0.1f, 0);
                        WeaponManager.Instance.CurrentlyEquippedWeapon.gameObject.GetComponent<SpriteRenderer>().flipY = false;
                    } else if (WeaponManager.Instance.CurrentlyEquippedWeapon.name == "Shotgun") {
                        this.transform.localPosition = new Vector3(-0.58f, -0.09f, 0);
                        WeaponManager.Instance.CurrentlyEquippedWeapon.gameObject.GetComponent<SpriteRenderer>().flipY = true;
                    }
                }
                if (WeaponManager.Instance.CurrentlyEquippedWeapon.transform.eulerAngles.z >= 89 && WeaponManager.Instance.CurrentlyEquippedWeapon.transform.eulerAngles.z <= 179) {
                    
                    controller.SetGunDirection(GunDirection.NW);
                    controller.SetPlayerDirection(PlayerDirection.BACK);

                    if(WeaponManager.Instance.CurrentlyEquippedWeapon.name == "Pistol") {
                        this.transform.localPosition = new Vector3(-0.87f, 0.1f, 0);
                        WeaponManager.Instance.CurrentlyEquippedWeapon.gameObject.GetComponent<SpriteRenderer>().flipY = false;
                    } else if (WeaponManager.Instance.CurrentlyEquippedWeapon.name == "Shotgun") {
                        this.transform.localPosition = new Vector3(-0.58f, -0.09f, 0);
                        WeaponManager.Instance.CurrentlyEquippedWeapon.gameObject.GetComponent<SpriteRenderer>().flipY = true;
                    }

                }
                if (WeaponManager.Instance.CurrentlyEquippedWeapon.transform.eulerAngles.z >= 269 && WeaponManager.Instance.CurrentlyEquippedWeapon.transform.eulerAngles.z <= 360) {
                    
                    controller.SetGunDirection(GunDirection.SE);
                    controller.SetPlayerDirection(PlayerDirection.FRONT);

                    if (WeaponManager.Instance.CurrentlyEquippedWeapon.name == "Pistol") {
                        this.transform.localPosition = new Vector3(0.64f, -0.12f, 0);
                        WeaponManager.Instance.CurrentlyEquippedWeapon.gameObject.GetComponent<SpriteRenderer>().flipY = true;
                    } else if (WeaponManager.Instance.CurrentlyEquippedWeapon.name == "Shotgun") {
                        this.transform.localPosition = new Vector3(0.15f, -0.35f, 0);
                        WeaponManager.Instance.CurrentlyEquippedWeapon.gameObject.GetComponent<SpriteRenderer>().flipY = false;
                    }
                }
            } else {
                transform.rotation = Quaternion.Euler(0f, 0f, rotaionZ);

                if (transform.eulerAngles.z >= 0 && transform.eulerAngles.z <= 89) {
                    controller.SetGunDirection(GunDirection.NE);
                    controller.SetPlayerDirection(PlayerDirection.BACK);
                } else if (transform.eulerAngles.z >= 90 && transform.eulerAngles.z <= 179) {
                    controller.SetGunDirection(GunDirection.NW);
                    controller.SetPlayerDirection(PlayerDirection.BACK);
                } else if (transform.eulerAngles.z >= 180 && transform.eulerAngles.z <= 269) {
                    controller.SetGunDirection(GunDirection.SW);
                    controller.SetPlayerDirection(PlayerDirection.FRONT);
                } else if (transform.eulerAngles.z >= 270 && transform.eulerAngles.z <= 360) {
                    controller.SetGunDirection(GunDirection.SE);
                    controller.SetPlayerDirection(PlayerDirection.FRONT);
                } 
            }
        } 
    }

    public void MoveCrosshair() {
        Vector3 aim = new Vector3(Input.GetAxis("MouseX"), Input.GetAxis("MouseY"), 0.0f);

        if(aim.magnitude > 0.0f) {
            aim.Normalize();
            aim *= 1.5f;
            CrossHair.transform.localPosition = aim;
        }
    }
}
