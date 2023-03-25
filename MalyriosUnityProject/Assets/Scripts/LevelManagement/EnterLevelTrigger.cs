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

    private LevelManager levelManager;


    void Start()
    {
        print($"{sceneName} entrance loaded(1)");
        player = GameObject.FindGameObjectWithTag("Player").transform;
        levelManager = GameObject.Find("GameManager").GetComponent<LevelManager>();
        interactableText = ReferencesManager.instance.interactableText;//GameObject.FindWithTag("InteractableText").GetComponent<TextMeshProUGUI>();
        print($"{sceneName} entrance loaded(2)");
    }
    public void Interact()
    {
        levelManager.ChangeLevel(sceneName);
    }

    private void Update()
    {
        print($"interactableText: {interactableText}");
    }

    private void ShowEnterDialog()
    {
        
        interactableText.text = $"Enter {sceneName}";
        interactableText.gameObject.SetActive(true);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        print("Collider Detected");
        if (other.gameObject.CompareTag("Player"))
        {
            ShowEnterDialog();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        interactableText.gameObject.SetActive(false);
    }
}
