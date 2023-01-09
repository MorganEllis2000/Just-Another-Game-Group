// WRITTEN BY: MORGAN ELLIS

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Used to heal the player, if they press 'F' while in the trigger and the healing station 
/// has enough uses left then they will be healed up to the max health of the player
/// </summary>
public class HealingStation : MonoBehaviour
{
    public int HealAmmount;
    [SerializeField] private GameObject HealText;
    public int NoOfUses;

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Player")) {
            HealText.SetActive(true);
        }
    }

    private void OnTriggerStay2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Player")) {
            if (Input.GetKey(KeyCode.F) && NoOfUses > 0) {
                if((PlayerController.Instance.Health + HealAmmount) > PlayerController.Instance.MaxHealth) {
                    PlayerController.Instance.Health = PlayerController.Instance.MaxHealth;
                    NoOfUses -= 1;
                } else if ((PlayerController.Instance.Health + HealAmmount) < PlayerController.Instance.MaxHealth) {
                    PlayerController.Instance.Health += HealAmmount;
                    NoOfUses -= 1;
                }
                this.GetComponent<AudioSource>().Play();
                StartCoroutine(PlayerController.Instance.ChangeSpriteColour(Color.green, 3f));
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Player")) {
            HealText.SetActive(false);
        }
    }
}
