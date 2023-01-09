using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] protected float fireRate;
    [SerializeField] public float MaxAmmo;
    [SerializeField] protected float ReloadTime;
    //[SerializeField] protected float CoolDownRate;
    [SerializeField] public float CurrentAmmo;
    [SerializeField] protected float Damage;
    [SerializeField] protected bool Reloading;
    [SerializeField] protected bool IsCoolingDown;
    [SerializeField] public bool CanShoot;

    [SerializeField] protected Transform FirePoint;

    protected AudioSource source;
    [SerializeField] protected AudioClip ShootingSound;
    [SerializeField] protected AudioClip ReloadingSound;

    protected void InstatiatePistolAmmo(Bullet ammo) {
        ammo.transform.rotation = FirePoint.rotation;
        Instantiate(ammo, FirePoint.position, FirePoint.rotation);
        ammo.target = GunRotate.Instance.CrossHair.transform.position;
    }
}
