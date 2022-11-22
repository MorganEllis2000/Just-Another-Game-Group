using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public static WeaponManager Instance { get; private set; }

    public GameObject[] OneHandedWeapos;

    public List<GameObject> Weapons = new List<GameObject>();

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
        if (Input.GetKeyDown(KeyCode.Alpha1)) {
            if (CurrentlyEquippedWeapon != null) {
                CurrentlyEquippedWeapon.SetActive(false);
            }    
            Weapons[0].SetActive(true);
            CurrentlyEquippedWeapon = Weapons[0];
        } else if (Input.GetKeyDown(KeyCode.Alpha2)) {
            if (CurrentlyEquippedWeapon != null) {
                CurrentlyEquippedWeapon.SetActive(false);
            }
            Weapons[1].SetActive(true);
            CurrentlyEquippedWeapon = Weapons[1];
        } else if (Input.GetKeyDown(KeyCode.Alpha3)) {
            if (CurrentlyEquippedWeapon != null) {
                CurrentlyEquippedWeapon.SetActive(false);
            }
            Weapons[2].SetActive(true);
            CurrentlyEquippedWeapon = Weapons[2];
        }

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
