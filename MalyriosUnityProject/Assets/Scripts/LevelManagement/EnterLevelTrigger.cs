using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnterLevelTrigger : MonoBehaviour, IInteractable
{
    [SerializeField] TextMeshProUGUI tmpText = null;
    [SerializeField] private int sceneIndex;
    [SerializeField] private string sceneName;
    public void Interact()
    {
        SceneManager.LoadScene(sceneIndex);
        print("Interacting with " + gameObject.name);
    }

    


    // Update is called once per frame
    void Update()
    {
        
    }
    private void ShowPickUpDialog()
    {
        tmpText.text = $"Enter {sceneName}";
        tmpText.gameObject.SetActive(true);
        print("Entering " + sceneName);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            ShowPickUpDialog();
        }
    }
}
