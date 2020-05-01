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
    [SerializeField] bool enterAutomatic = false;
    [SerializeField] Vector3 spawnPoint = new Vector3(0,-20,0);
    Scene currentScene;

    bool colliding;
    GameObject[] LevelLoaderColliders;

    private void Start()
    {
        LevelLoaderColliders = GameObject.FindGameObjectsWithTag("LevelLoader");
    }

    private void Update()
    {

        currentScene = SceneManager.GetActiveScene();

        //Disable other colliders with this script to avoid wrong beahviour
        if (colliding)
        {

            foreach (GameObject levelLoader in LevelLoaderColliders)
            {
                if (levelLoader == gameObject)
                {
                    levelLoader.SetActive(true);
                }
                else
                {
                    levelLoader.SetActive(false);
                }
            }

            textMeshProUGUI.text = displayText;
            textMeshProUGUI.gameObject.SetActive(true);
            if (Player.interactInput || enterAutomatic)
            {
                //Set point where to spawn in the loading scene
                StaticData.spawnPoint = spawnPoint;

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
        foreach (GameObject levelLoader in LevelLoaderColliders)
        {
            if (levelLoader != gameObject)
            {

                levelLoader.SetActive(true);
            }
        }
    }

}
