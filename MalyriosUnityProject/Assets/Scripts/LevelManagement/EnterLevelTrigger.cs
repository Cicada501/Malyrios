using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnterLevelTrigger : MonoBehaviour, IInteractable
{
    Transform player;
    TextMeshProUGUI interactableText = null;
    [SerializeField] private string sceneName;
    [SerializeField] Transform spawnPoint;

    private LevelManager levelManager;


    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        levelManager = GameObject.Find("GameManager").GetComponent<LevelManager>();
        interactableText = GameObject.Find("InteractableText").GetComponent<TextMeshProUGUI>();
    }
    public void Interact()
    {
        levelManager.ChangeLevel(sceneName);
    }

    private void ShowEnterDialog()
    {
        
        interactableText.text = $"Enter {sceneName}";
        interactableText.gameObject.SetActive(true);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            ShowEnterDialog();
            print("HERE");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        interactableText.gameObject.SetActive(false);
    }
}
