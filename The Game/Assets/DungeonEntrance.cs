using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DungeonEntrance : MonoBehaviour
{
    [SerializeField] private LoadingScene loadingScene;
    public GameObject EnterText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Player")) {
            EnterText.SetActive(true);
            if (Input.GetKey(KeyCode.F)) {
                loadingScene.LoadScene(3);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Player")) {
            EnterText.SetActive(false);
        }
    }
}
