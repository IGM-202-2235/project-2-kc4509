using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadScene : MonoBehaviour
{
    public void startGame(){
        SceneManager.LoadScene("Scenes/Instructions");
    }
    public void letsgo(){
        SceneManager.LoadScene("Scenes/Test");
    }
    public void homeScreen(){
        SceneManager.LoadScene("Scenes/Home");
    }
}