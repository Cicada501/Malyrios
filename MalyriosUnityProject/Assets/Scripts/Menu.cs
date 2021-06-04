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
    public void changeMenuOpen(){
        if(MenuPanel.activeSelf == false){

            MenuPanel.SetActive(true);
        }
        else
        {
            MenuPanel.SetActive(false);
        }
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
}
