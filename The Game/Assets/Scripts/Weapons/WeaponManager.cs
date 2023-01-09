// WRITTEN BY: MORGAN ELLIS

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// This script is responsible for managing the players weapon as well as
/// allowing them to switch weapons using the 1 and 2 keys
/// </summary>
public class WeaponManager : MonoBehaviour
{
    public static WeaponManager Instance { get; private set; }

    public GameObject[] OneHandedWeapos;

    public List<GameObject> Weapons = new List<GameObject>();

    [SerializeField] public GameObject CurrentlyEquippedWeapon;

    [SerializeField] private Image WeaponUI;
    [SerializeField] private Sprite PistolUI;
    [SerializeField] private Sprite ShotgunUI;
    [SerializeField] private TextMeshProUGUI AmmoCounter;
    

    private void Awake() {

        if (Instance != null && Instance != this) {
            Destroy(gameObject);
        }

        Instance = this;

        DontDestroyOnLoad(this);
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(CurrentlyEquippedWeapon == null) {
            WeaponUI.gameObject.SetActive(false);
        } else {
            WeaponUI.gameObject.SetActive(true);
        }
        if (CurrentlyEquippedWeapon != null) {
            AmmoCounter.text = CurrentlyEquippedWeapon.GetComponent<Weapon>().CurrentAmmo + " / " + CurrentlyEquippedWeapon.GetComponent<Weapon>().MaxAmmo;
        }
            

        if (Input.GetKeyDown(KeyCode.Alpha1)) {
            if (CurrentlyEquippedWeapon != null) {
                CurrentlyEquippedWeapon.SetActive(false);
            }    
            Weapons[0].SetActive(true);
            PlayerController.Instance.SetOneHandedWeapon(OneHandedWeapons.PISTOL);
            PlayerController.Instance.UpdatePlayerOneHandedWeapon();
            CurrentlyEquippedWeapon = Weapons[0];
            CurrentlyEquippedWeapon.GetComponent<Pistol>().CanShoot = true;
            WeaponUI.sprite = PistolUI; 
        } else if (Input.GetKeyDown(KeyCode.Alpha2)) {
            if (CurrentlyEquippedWeapon != null) {
                CurrentlyEquippedWeapon.SetActive(false);
            }
            Weapons[1].SetActive(true);
            CurrentlyEquippedWeapon = Weapons[1];
            PlayerController.Instance.SetOneHandedWeapon(OneHandedWeapons.PISTOL);
            PlayerController.Instance.UpdatePlayerOneHandedWeapon();
            CurrentlyEquippedWeapon.GetComponent<Shotgun>().CanShoot = true;
            WeaponUI.sprite = ShotgunUI;

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
