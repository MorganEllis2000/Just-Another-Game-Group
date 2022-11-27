using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] protected float fireRate;
    [SerializeField] protected float MaxAmmo;
    [SerializeField] protected float ReloadTime;
    //[SerializeField] protected float CoolDownRate;
    [SerializeField] protected float CurrentAmmo;
    [SerializeField] protected float Damage;
    [SerializeField] protected bool Reloading;
    [SerializeField] protected bool IsCoolingDown;
    [SerializeField] protected bool CanShoot;

    [SerializeField] protected Transform FirePoint; 

    protected void InstatiatePistolAmmo(Bullet ammo) {
        ammo.transform.rotation = FirePoint.rotation;
        Instantiate(ammo, FirePoint.position, FirePoint.rotation);
        ammo.target = GunRotate.Instance.CrossHair.transform.position;
    }
}
