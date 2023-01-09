// WRITTEN BY: MORGAN ELLIS

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

/// <summary>
/// Used to fade in and out the sprites that cover each room in the dungeon
/// </summary>
public class FadeSprite : MonoBehaviour
{
    [SerializeField] private GameObject[] doors;

    IEnumerator FadeTo(float aValue, float aTime) {
        float alpha = transform.GetComponent<SpriteRenderer>().color.a;
        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / aTime) {
            Color newColor = new Color(1, 1, 1, Mathf.Lerp(alpha, aValue, t));
            transform.GetComponent<SpriteRenderer>().color = newColor;
            yield return null;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Player")) {
            StartCoroutine(FadeTo(0.0f, 1.0f));

            for(int i = 0; i < doors.Length; i++) {
                doors[i].SetActive(true);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Player")) {
            StartCoroutine(FadeTo(1.0f, 1.0f));
        }
    }
}
