using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{   
    public void OnButtonMenu()
    {        
        SceneManager.LoadScene("Menu");
    }
    public void OnButtonCredits()
    {        
        SceneManager.LoadScene("Credits");
    }
    public void OnButtonInstructions()
    {        
        SceneManager.LoadScene("Instructions");
    }
    public void OnButtonPlay()
    {
        SceneManager.LoadScene("Level1");
    }

}
