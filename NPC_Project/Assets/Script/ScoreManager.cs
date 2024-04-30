using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    // Start is called before the first frame update
    public TextMeshProUGUI Blue;
    public TextMeshProUGUI Red;
    public TextMeshProUGUI timerText;
    public SpawnManager spawnManager;
    public float timerCounter = 10.0f;
    int redScore;
    int blueScore;
    public 
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer();
        score();
    }

    void score(){
        blueScore = 0;
        redScore = 0;
        foreach(Agent fish in spawnManager.spawnedFishes){
            if(fish.spriteRenderer.color == Color.blue){
                blueScore++;
            }
            else if (fish.spriteRenderer.color == Color.red){
                redScore++;
            }
        }
        Red.text = "Red: " + redScore.ToString();
        Blue.text = "Blue: " + blueScore.ToString();
    }

    void timer(){
        timerCounter -= Time.deltaTime;
        timerText.text = "TIME: " + timerCounter.ToString();
        if(timerCounter <= 0.0f){
            if(redScore < blueScore){
                SceneManager.LoadScene("Scenes/BlueWin");
                timerCounter = 60.0f;
            }
            else if(blueScore < redScore){
                SceneManager.LoadScene("Scenes/RedWin");
                timerCounter = 60.0f;
            }
            else if(blueScore == redScore){
                SceneManager.LoadScene("Scenes/Tie");
                timerCounter = 60.0f;
            }
            else{
                SceneManager.LoadScene("Scenes/Home");
                timerCounter = 60.0f;
            }

        }
    }
}
