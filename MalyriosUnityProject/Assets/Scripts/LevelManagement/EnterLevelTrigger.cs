using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnterLevelTrigger : MonoBehaviour, IInteractable
{
    Transform player;
    TextMeshProUGUI interactableText = null;
    [SerializeField] private string levelName;

    private LevelManager levelManager;
    private SaveActiveItems activeItemsData;
    [SerializeField] private bool highForestPortal;
    private SpriteRenderer spriteRenderer;
    private BoxCollider2D boxCollider2D;


    void Start()
    {
        player = ReferencesManager.Instance.player.transform;
        levelManager = GameObject.Find("GameManager").GetComponent<LevelManager>();
        interactableText = ReferencesManager.Instance.interactableText;
        activeItemsData = ReferencesManager.Instance.saveActiveItems;
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider2D = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        if (highForestPortal)
        {
            bool isActive = ExtractNumber(levelName) <= LevelUnlock.Instance.unlockedLevel;
            spriteRenderer.enabled = isActive;
            boxCollider2D.enabled = isActive;
        }
    }

    public void Interact()
    {
        activeItemsData.SaveItems();
        ReferencesManager.Instance.levelManager.ShowLoadingScreen(levelName);
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

    public static int ExtractNumber(string input)
    {
        Regex regex = new Regex(@"\d+");
        Match match = regex.Match(input);
        if (match.Success)
        {
            return int.Parse(match.Value);
        }
        else
        {
            return -1;
        }
    }
}