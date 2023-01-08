using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;

public class TriggerTimeline : MonoBehaviour
{

    public GameObject timeline;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator PausePlayer() {
        PlayerController.Instance.IsTalking = true;
        PlayerController.Instance.rigidBody2D.velocity = Vector2.zero;
        PlayerController.Instance.GetComponent<Animator>().SetBool("IsRunning", false);
        PlayerController.Instance.GetComponent<Animator>().SetInteger("SetGunDirection", 4);
        //PlayerController.Instance.GetComponent<Animator>().SetBool("ReturnToIdle", true);
        yield return new WaitForSeconds(1);
        timeline.SetActive(true);
        yield return new WaitForSeconds(6);
        PlayerController.Instance.IsTalking = false;
        Destroy(this.gameObject);

    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Player")) {
            StartCoroutine(PausePlayer());
        }
    }

    //private void OnTriggerStay2D(Collider2D collision) {
    //    if (collision.CompareTag("Player")) {
    //        PlayerController.Instance.IsTalking = true;
    //        PlayerController.Instance._Horizontal = 0;
    //        PlayerController.Instance._Vertical = 0;
    //    }
    //}


    //private void OnTriggerExit2D(Collider2D collision) {
    //    if (collision.CompareTag("Player")) {          
    //        PlayerController.Instance.IsTalking = false;
    //        Destroy(this.gameObject);
    //    }
    //}
}
