using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour

{
    [SerializeField] GameObject MenuPanel;
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

    public void respawn(){

    }
}
