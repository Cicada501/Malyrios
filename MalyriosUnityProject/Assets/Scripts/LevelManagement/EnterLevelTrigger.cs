using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnterLevelTrigger : MonoBehaviour, IInteractable
{
    Player player;
    private Inventory inventory;
    [SerializeField] TextMeshProUGUI tmpText = null;
    [SerializeField] private string sceneName;
    [SerializeField] Transform spawnPoint;
    public void Interact()
    {
        /*SaveSystem.SavePlayer(player);
        SaveSystem.SaveInventory(inventory);
        SceneManager.LoadScene(sceneIndex);*/
        player.transform.position = spawnPoint.position;
        //print("Interacting with " + gameObject.name);
    }

    void Start()
    {
        player = FindObjectOfType<Player>();
        inventory = FindObjectOfType<Inventory>();
    }


    // Update is called once per frame
    void Update()
    {
        
    }
    private void ShowEnterDialog()
    {
        tmpText.text = $"Enter {sceneName}";
        tmpText.gameObject.SetActive(true);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            ShowEnterDialog();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        tmpText.gameObject.SetActive(false);
    }
}
