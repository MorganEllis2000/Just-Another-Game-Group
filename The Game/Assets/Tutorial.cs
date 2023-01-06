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
    public int TutorialCounter = 1;

    public bool IsTutorialFinished = false;

    private void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(gameObject);
        }

        Instance = this;
    }

    public TextMeshProUGUI TutorialText;

    void Start()
    {
        StartCoroutine(WaitForScreen());
    }

    private IEnumerator WaitForScreen() {
        PlayerController.Instance.IsTalking = true;
        yield return new WaitForSeconds(10f);
        PlayerController.Instance.IsTalking = false;
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
        }
    }

    public void WalkDown() {
        TutorialText.text = "Press 'S' to walk down";
        PlayerController.Instance._Horizontal = 0;
        PlayerController.Instance._Vertical = 0;
        PlayerController.Instance.canDash = false;

        if (Input.GetKey(KeyCode.S)) {
            PlayerController.Instance._Vertical = -1;
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
        }

        if (PlayerController.Instance.transform.position.y > StartPos.y) {
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
        }

        if (PlayerController.Instance.transform.position.x < StartPos.x - 2.5f) {
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
        }

        if (PlayerController.Instance.transform.position.x > StartPos.x) {
            TutorialCounter += 1;
        }
    }

    public void DashTutorial() {
        TutorialText.text = "";
        PlayerController.Instance.canDash = false;
        if (Vector3.Distance(GameObject.Find("Branch(Clone)").transform.position, PlayerController.Instance.transform.position) < 4.0f){
            Time.timeScale = 0f;
            TutorialText.text = "Press 'Left Shift' and 'S' to dash";

            if(Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.S)) {
                Time.timeScale = 1.0f;
                PlayerController.Instance.canDash = true;
                StartCoroutine(PlayerController.Instance.Dash());
                TutorialText.text = "";
                TutorialCounter += 1;
            }
        }


    }
}
