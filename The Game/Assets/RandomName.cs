using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RandomName : MonoBehaviour
{
    public Text IDName;
    void Start()
    {
        IDName.text = GenerateRandomCharAZ() + "." + GenerateRandomCharAZ() + "." + " " + GenerateRandomCharAZ() + GenerateRandomCharAZ() + GenerateRandomCharAZ() + Random.Range(01, 99).ToString();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public char GenerateRandomCharAZ() {
        return (char)Random.Range('a', 'z');
    }
}
