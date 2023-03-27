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
    [SerializeField] private string levelName;

    private LevelManager levelManager;


    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        levelManager = GameObject.Find("GameManager").GetComponent<LevelManager>();
        interactableText = ReferencesManager.Instance.interactableText;

    }
    public void Interact()
    {
        levelManager.ChangeLevel(levelName);
    }


    private void ShowEnterDialog()
    {
        
        interactableText.text = $"Enter {levelName}";
        interactableText.gameObject.SetActive(true);
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
        interactableText.gameObject.SetActive(false);
    }
}
