using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OxygenManager : MonoBehaviour
{
    public Image OxygenBar;
    void Start()
    {
        InvokeRepeating("DecreaseOxygen", 1.0f, 1.0f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DecreaseOxygen() {
        PlayerController.Instance.Oxygen -= 1;
        OxygenBar.GetComponent<RectTransform>().sizeDelta = new Vector2(OxygenBar.GetComponent<RectTransform>().sizeDelta.x - (OxygenBar.GetComponent<RectTransform>().sizeDelta.x / PlayerController.Instance.Oxygen) , 40);
        
    }
}
