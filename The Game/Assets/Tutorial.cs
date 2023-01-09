using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using static UnityEditor.Searcher.SearcherWindow.Alignment;
#endif
using UnityEngine.UI;
using TMPro;

public class Tutorial : MonoBehaviour
{

    public static Tutorial Instance { get; private set; }

    public Vector3 StartPos;
    public int TutorialCounter = 0;

    public bool IsTutorialFinished = false;

    [SerializeField] private AudioClip[] AIVoices;
    private AudioSource source;

    private void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(gameObject);
        }

        Instance = this;
    }

    public TextMeshProUGUI TutorialText;

    public GameObject PostTutorialTimeline;

    void Start()
    {
        StartCoroutine(WaitForScreen());
        source = this.GetComponent<AudioSource>();
    }

    private IEnumerator WaitForScreen() {
        PlayerController.Instance.IsTalking = true;
        yield return new WaitForSeconds(9.5f);
        PlayerController.Instance.IsTalking = false;
        TutorialCounter += 1;
    }

    void Update()
    {
        if(TutorialCounter == 1) {
            WalkDown();
        } else if (TutorialCounter == 2) {
            WalkUp();
        } else if (TutorialCounter == 3) {
            WalkLeft();
        } else if (TutorialCounter == 4) {
            WalkRight();
        } else if (TutorialCounter == 5) {
            DashTutorial();
        } else if (TutorialCounter == 6 && LevelManager.Instance.NumberOfEnemies == 6) {
            StartCoroutine(PostDashDialogue());
        }

        Debug.Log("Horizontal: " + PlayerController.Instance._Horizontal);
        Debug.Log("Vertical: " + PlayerController.Instance._Vertical);
        Debug.Log("Velocity: " + PlayerController.Instance.rigidBody2D.velocity);
    }

    public void WalkDown() {

        TutorialText.text = "Press 'S' to walk down";
        PlayerController.Instance._Horizontal = 0;
        PlayerController.Instance._Vertical = 0;
        PlayerController.Instance.canDash = false;


        if (Input.GetKey(KeyCode.S)) {
            PlayerController.Instance._Vertical = -1;
            PlayerController.Instance.rigidBody2D.velocity = new Vector2(PlayerController.Instance._Horizontal * PlayerController.Instance.runSpeed, PlayerController.Instance._Vertical * PlayerController.Instance.runSpeed);
        } else {
            PlayerController.Instance.rigidBody2D.velocity = Vector2.zero;
         }

        if (PlayerController.Instance.transform.position.y < StartPos.y - 2.5f) {
            TutorialCounter += 1;
        }
    }

    public void WalkUp() {


        TutorialText.text = "Press 'W' to walk up";
        PlayerController.Instance._Horizontal = 0;
        PlayerController.Instance._Vertical = 0;
        PlayerController.Instance.canDash = false;

        if (Input.GetKey(KeyCode.W)) {
            PlayerController.Instance._Vertical = 1;
            PlayerController.Instance.rigidBody2D.velocity = new Vector2(PlayerController.Instance._Horizontal * PlayerController.Instance.runSpeed, PlayerController.Instance._Vertical * PlayerController.Instance.runSpeed);
        } else {
            PlayerController.Instance.rigidBody2D.velocity = Vector2.zero;
        }

        if (PlayerController.Instance.transform.position.y > StartPos.y) {
            source.Stop();
            TutorialCounter += 1;
        }
    }

    public void WalkLeft() {

        TutorialText.text = "Press 'A' to walk left";
        PlayerController.Instance._Horizontal = 0;
        PlayerController.Instance._Vertical = 0;
        PlayerController.Instance.canDash = false;
        

        if (Input.GetKey(KeyCode.A)) {
            PlayerController.Instance._Horizontal = -1;
            PlayerController.Instance.rigidBody2D.velocity = new Vector2(PlayerController.Instance._Horizontal * PlayerController.Instance.runSpeed, PlayerController.Instance._Vertical * PlayerController.Instance.runSpeed);
        } else {
            PlayerController.Instance.rigidBody2D.velocity = Vector2.zero;
         }

        if (PlayerController.Instance.transform.position.x < StartPos.x - 2.5f) {
            source.Stop();
            TutorialCounter += 1;
        }
    }

    public void WalkRight() {
       

        TutorialText.text = "Press 'D' to walk right";
        PlayerController.Instance._Horizontal = 0;
        PlayerController.Instance._Vertical = 0;
        PlayerController.Instance.canDash = false;

        if (Input.GetKey(KeyCode.D)) {
            PlayerController.Instance._Horizontal = 1;
            PlayerController.Instance.rigidBody2D.velocity = new Vector2(PlayerController.Instance._Horizontal * PlayerController.Instance.runSpeed, PlayerController.Instance._Vertical * PlayerController.Instance.runSpeed);
        }

        if (PlayerController.Instance.transform.position.x > StartPos.x) {
            source.Stop();
            TutorialCounter += 1;
        }
    }

    public void DashTutorial() {
        TutorialText.text = "";
        PlayerController.Instance.canDash = false;
        if(GameObject.Find("Branch(Clone)") != null) {
            if (Vector3.Distance(GameObject.Find("Branch(Clone)").transform.position, PlayerController.Instance.transform.position) < 4.0f) {
                if (source.isPlaying == false) {
                    source.clip = AIVoices[Random.Range(0, 2)];
                    source.Play();
                }

                Time.timeScale = 0f;
                TutorialText.text = "Press 'Left Shift' and a direction to dash";

                if ((Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.S)) || (Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.D)) || (Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.A)) || (Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.W))) {
                    Time.timeScale = 1.0f;
                    PlayerController.Instance.canDash = true;
                    StartCoroutine(PlayerController.Instance.Dash());
                    TutorialText.text = "";
                    TutorialCounter += 1;
                }
            }
        }

    }

    public IEnumerator PostDashDialogue() {
        TutorialCounter++; 
        PostTutorialTimeline.gameObject.SetActive(true);
        PlayerController.Instance.IsTalking = true;
        PlayerController.Instance.rigidBody2D.velocity = Vector2.zero;
        //PlayerController.Instance.GetComponent<Animator>().SetBool("IsRunning", false);
        PlayerController.Instance.GetComponent<Animator>().Play("Base Layer.A_Player_Idle_FRONT");
        yield return new WaitForSeconds(7f);
        PlayerController.Instance.IsTalking = false;
    }
}
