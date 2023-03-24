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

    private LevelManager levelManager;


    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        levelManager = GameObject.Find("GameManager").GetComponent<LevelManager>();
    }
    public void Interact()
    {
        levelManager.ChangeLevel(sceneName);
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
