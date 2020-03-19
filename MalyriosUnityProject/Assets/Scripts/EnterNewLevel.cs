using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class EnterNewLevel : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI textMeshProUGUI;
    [SerializeField]
    string sceneToEnter;
    Scene currentScene;
    [SerializeField]
    string displayText;

    bool colliding;

    //Transform player;




    bool ePressed = false;

    private void Start()
    {
        
        //player = GameObject.FindGameObjectWithTag("Player").transform;



    }

    private void Update()
    {
        ePressed = Input.GetKey(KeyCode.E);
        currentScene = SceneManager.GetActiveScene();
        print(currentScene.name);
        if(colliding){
            textMeshProUGUI.text = displayText;
            textMeshProUGUI.gameObject.SetActive(true);
            if (ePressed)
            {
                StaticData.playerPutToSpawnPoint = false;
                
                if (sceneToEnter == "Cliffs" && currentScene.name == "Cave")
                {
                    StaticData.spawnPoint = new Vector3(-2.09f, -20f, 0f);

                }
                else if (sceneToEnter == "Cave" && currentScene.name == "Cliffs")
                {
                    print("Cliffs to Cave");
                    StaticData.spawnPoint = new Vector3(-1f, 0f, 0f);

                }else if (sceneToEnter == "Cliffs" && currentScene.name == "Wood")
                {
                    print("Cliffs to Cave");
                    StaticData.spawnPoint = new Vector3(3.5f, -6.5f, 0f);

                }else if (sceneToEnter == "Wood" && currentScene.name == "Cliffs")
                {
                    print("Cliffs to Cave");
                    StaticData.spawnPoint = new Vector3(0, 0f, 0f);

                }
                SceneManager.LoadScene(sceneToEnter);
            }
        }else{
                textMeshProUGUI.gameObject.SetActive(false);
            }

        

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            colliding = true;           
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {          
            colliding = false;
        }
    }

}
