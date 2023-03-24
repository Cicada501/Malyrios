using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnterLevelTrigger : MonoBehaviour, IInteractable
{
    Transform player;
    private Inventory inventory;
    [SerializeField] TextMeshProUGUI tmpText = null;
    [SerializeField] private string sceneName;
    [SerializeField] Transform spawnPoint;


    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    public void Interact()
    {
        LevelManager.ChangeLevel(sceneName);

        //player.position = enterLevelPosition.position;

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
