using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class EnterNewLevel : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI textMeshProUGUI;
    [SerializeField] string sceneToEnter;
    [SerializeField] string displayText;
    Scene currentScene;

    bool colliding;

    [SerializeField] GameObject triggerCollider1;
    [SerializeField] GameObject triggerCollider2;

    //Transform player;




    private void Start()
    {

        //player = GameObject.FindGameObjectWithTag("Player").transform;



    }

    private void Update()
    {

        currentScene = SceneManager.GetActiveScene();

        //Disable other colliders with this script to avoid wrong beahviour
        if (colliding)
        {
            if (triggerCollider1 && triggerCollider2)
            {
                if (gameObject == triggerCollider2)
                {

                    triggerCollider1.SetActive(false);
                }
                else if (gameObject == triggerCollider1)
                {
                    triggerCollider2.SetActive(false);
                }
            }
            textMeshProUGUI.text = displayText;
            textMeshProUGUI.gameObject.SetActive(true);
            if (Player.interactInput){
            
                //Set Spawnpoints to the right spot for the depending level
                if (sceneToEnter == "Cliffs" && currentScene.name == "Cave")
                {
                    StaticData.spawnPoint = new Vector3(-2.09f, -20f, 0f);

                }
                else if (sceneToEnter == "Cave" && currentScene.name == "Cliffs")
                {

                    StaticData.spawnPoint = new Vector3(-1f, 0f, 0f);

                }
                else if (sceneToEnter == "Cliffs" && currentScene.name == "Wood")
                {

                    StaticData.spawnPoint = new Vector3(3.5f, -6.5f, 0f);

                }
                else if (sceneToEnter == "Wood" && currentScene.name == "Cliffs")
                {

                    StaticData.spawnPoint = new Vector3(0, 0f, 0f);

                }

                //Set static items list to inventry items list
                StaticData.itemsStatic = Inventory.Instance.items;
                Inventory.itemsLoaded = false;

                LevelLoader.levelToLoad = sceneToEnter;
                LevelLoader.loadLevel = true;
            }// END: Interacting
        }




    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {

            colliding = true;
            textMeshProUGUI.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            colliding = false;
            textMeshProUGUI.gameObject.SetActive(false);
        }
        if (triggerCollider2 && triggerCollider1)
        {
            triggerCollider2.SetActive(true);
            triggerCollider1.SetActive(true);
        }
    }

}
