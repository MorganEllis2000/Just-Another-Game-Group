// WRITTEN BY: MORGAN ELLIS

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This generates a random name for the player each time the game is restared 
/// The name takes the form of A.A. AAA00 where A represents any letter between A -Z 
/// and the 00 represents any number between 01 and 99
/// </summary>
public class RandomName : MonoBehaviour
{
    public Text IDName;
    void Start()
    {
        IDName.text = GenerateRandomCharAZ() + "." + GenerateRandomCharAZ() + "." + " " + GenerateRandomCharAZ() + GenerateRandomCharAZ() + GenerateRandomCharAZ() + Random.Range(01, 99).ToString();
    }


    public char GenerateRandomCharAZ() {
        return (char)Random.Range('a', 'z');
    }
}
