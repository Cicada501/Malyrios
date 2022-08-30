using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Menu : MonoBehaviour

{
    [SerializeField] GameObject MenuPanel;
    [SerializeField] TextMeshProUGUI PauseButtonText;

    private bool gamePaused = false;

    void Start(){
        MenuPanel.SetActive(false);
    }
    public void changeMenuOpen()
    {
        MenuPanel.SetActive(MenuPanel.activeSelf == false);
    }
    public void PauseGame(){
        if(!gamePaused){
            Time.timeScale = 0;
            gamePaused = true;
            PauseButtonText.text = "Resume";
        }
        else{
            Time.timeScale = 1;
            gamePaused = false;
            
            PauseButtonText.text = "Pause";
        }
        
    }
    
    public void QuitGame()
    {   
        Debug.Log("Quitting Game");
        Application.Quit();
    }
}
