using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEditor.Rendering;
using System.ComponentModel;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance { get; private set; }

    public TextMeshProUGUI NameText;
    public TextMeshProUGUI DialogueText;
    public GameObject continueButton;
    public GameObject DialoguePanel;

    private Queue<string> Names; // Creates a queue for the names that will be displayed 
    public Queue<string> Sentences; // Creates a queue for the sentacnes that will be displayed
    private string[] TempSentences; // Tempory string array that stores the sentances before being added to the appropriate list
    private string[] SplitTemp; // Tempory string array that stores the names before being added to the appropriate list
    public List<string> Dialogue = new List<string>(); // Stores the sentances from the current text file
    private List<string> slNames = new List<string>(); // Stores the names from the current text file

    public bool bIsDialogueOpen = false; // Bool that tracks if the dialgoue panel is open

    public bool bHasDialogueFinished = false; // Bool that tracks if the dialogue has finished.

    public Dialogue dialogue1;
    public bool dialogue1Activated = false;

    /// <summary>
    /// Create an instance of the dialogue manager
    /// </summary>
    private void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(gameObject);
        }

        Instance = this;
    }

    /// <summary>
    /// Create a new queue for our names and sentances
    /// </summary>
    private void Start() {
        Names = new Queue<string>();
        Sentences = new Queue<string>();
    }

    public float timer = 0f;
    public string CurrentTxtFileName = null;

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Space) && DialoguePanel.activeSelf) {
            DisplayNextSentence();
        }

        timer += Time.deltaTime;

        if (PlayerController.Instance.Health < 50 && PlayerController.Instance.Oxygen < PlayerController.Instance.MaxOxygen / 2 && dialogue1Activated == false) {
            timer = 0;
            StartDialogue(dialogue1);
            dialogue1Activated = true;
        }

        if (timer >= 3.0f && CurrentTxtFileName == "Dialogue_50%HealthOxygen") {
            DisplayNextSentence();
            timer = 0;
        }
    }

    public IEnumerator HasLessThan50HealthOxygen() {
        DisplayNextSentence();
        yield return new WaitForSeconds(3.0f);
    }

    public void StartDialogue(Dialogue dialogue) {
        DialoguePanel.SetActive(true);
        Names = new Queue<string>();
        Sentences = new Queue<string>();
       
        //bHasDialogueFinished = false;
        //dialoguePanel.SetActive(true);
        //bIsDialogueOpen = true;

        //animator.SetBool("bIsOpen", true);
        
        Names.Clear();
        Sentences.Clear();
        ParseTextFile(dialogue);
        CurrentTxtFileName = CurrentFileName(dialogue);

        DisplayNextSentence();
    }

    /// <summary>
    /// Function that is responsible for parsing the text file, every character up until the '|' while be added to the names list 
    /// while everything after the '|' that is on the same line will be added to the dialogue list. The value of these lists are the queued into their 
    /// respective queues
    /// </summary>
    /// <param name="dialogue"> Reference to the dialogue script where our text asset is assigned</param>
    private void ParseTextFile(Dialogue dialogue) {
        TempSentences = dialogue.Text.text.Split('\n');

        for (int i = 0; i < TempSentences.Length; i++) {
            SplitTemp = TempSentences[i].Split('|');
            slNames.Add(SplitTemp[0]);
            Dialogue.Add(SplitTemp[1]);
            Names.Enqueue(slNames[i]);
            Sentences.Enqueue(Dialogue[i]);
        }
    }

    public void DisplayNextSentence() {
        if (Sentences.Count == 0) {
            EndDialogue();
            return;
        }

        string name = Names.Dequeue();
        string sentence = Sentences.Dequeue();
        StopAllCoroutines();
        DialogueText.text = sentence;
        NameText.text = name;
        //StartCoroutine(TypeSentence(sentence));
    }

    public void EndDialogue() {
        DialoguePanel.SetActive(false);
        //bIsDialogueOpen = false;

        Dialogue.Clear();
        slNames.Clear();
        TempSentences = null;

        PlayerController.Instance.IsTalking = false;
        Time.timeScale = 1;

        //animator.SetBool("bIsOpen", false);
        //bHasDialogueFinished = true;
    }

    /// <summary>
    /// Helper function that can be called from other scripts to set that dialogue is finished
    /// </summary>
    /// <param name="finished"></param>
    public void SetDialogueFinished(bool finished) {
        bHasDialogueFinished = finished;
    }

    public string CurrentFileName(Dialogue dialogue) {
        return dialogue.Text.name.ToString();
    }

    /// <summary>
    /// Helper function that can be called from other scripts to see if the dialgoue has finished
    /// </summary>
    /// <returns></returns>
    public bool HasDialogueFinished() {
        return bHasDialogueFinished;
    }
}
