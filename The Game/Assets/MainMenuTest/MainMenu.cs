//WRITTEN BY: KISHEN 
//EDITED BY: MORGAN ELLIS

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
   
    public void PlayGame ()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);  
    }

    
    public void QuitGame ()
    {
        Application.Quit();
    }

    public void LoadScene(int index) {
        SceneManager.LoadScene(index);
    }

    public void DestroyPlayer() {
        Destroy(PlayerController.Instance.gameObject);
        Destroy(WeaponManager.Instance.gameObject);
    }
}
