using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTriggerStatic : MonoBehaviour
{
    public Dialogue dialogue;

    public void TriggerDialogue() {
        DialogueManager.Instance.DialoguePanel.SetActive(true);
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
        Destroy(this.gameObject);
    }

    public void CloseDialogue() {
        FindObjectOfType<DialogueManager>().EndDialogue();
        //this.gameObject.GetComponent<Animator>().SetBool("bTalking", false);
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        PlayerController.Instance.IsTalking = true;
        Time.timeScale = 0;
        TriggerDialogue();
    }
}