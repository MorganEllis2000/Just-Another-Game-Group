using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public static WeaponManager Instance { get; private set; }

    public GameObject[] OneHandedWeapos;

    public Dictionary<string, GameObject> Weapons;

    [SerializeField] public GameObject CurrentlyEquippedWeapon;

    private void Awake() {

        if (Instance != null && Instance != this) {
            Destroy(gameObject);
        }

        Instance = this;
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EquipOneHandedWeapon(GameObject weapon) {
        if(CurrentlyEquippedWeapon != null) {
            CurrentlyEquippedWeapon.SetActive(false);
            CurrentlyEquippedWeapon = null;
        }

        weapon.SetActive(true);
        if(weapon.name == "Pistol") {
            PlayerController.Instance.SetOneHandedWeapon(OneHandedWeapons.PISTOL);
        }
        PlayerController.Instance.UpdatePlayerOneHandedWeapon();
        CurrentlyEquippedWeapon = weapon;
    }
}
