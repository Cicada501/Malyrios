using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class ItemData
{
    public string itemName;
    public bool isActive;
    public string levelName;
}

[Serializable]
public class LevelItemsData
{
    public List<ItemData> items;
}

public class SaveActiveItems : MonoBehaviour
{
    LevelItemsData itemsData = new LevelItemsData();
    private LevelManager levelManager;

    private void Start()
    {
        levelManager = ReferencesManager.Instance.levelManager;
    }


    public void SaveItems()
    {
        if (PlayerPrefs.HasKey("levelItemsData") && PlayerPrefs.GetString("levelItemsData") != "")
        {
            string json = PlayerPrefs.GetString("levelItemsData");
            itemsData = JsonUtility.FromJson<LevelItemsData>(json);
        }
        else
        {
            itemsData = new LevelItemsData {items = new List<ItemData>()};
        }

        // Gehe durch die Items und speichere ihre Daten
        GameObject itemsParent = GameObject.Find("Items");
        if (itemsParent)
        {
            foreach (Transform item in itemsParent.transform)
            {
                string itemName = item.name;
                string currentLevelName = levelManager.GetCurrentLevelName();

                if (itemsData.items.Exists(i => i.itemName == itemName && i.levelName == currentLevelName))
                {
                    itemsData.items.RemoveAll(i => i.itemName == itemName && i.levelName == currentLevelName);

                    ItemData data = new ItemData
                    {
                        itemName = itemName,
                        isActive = item.gameObject.activeSelf,
                        levelName = currentLevelName
                    };
                    itemsData.items.Add(data);
                }
                else
                {
                    ItemData data = new ItemData
                    {
                        itemName = itemName,
                        isActive = item.gameObject.activeSelf,
                        levelName = currentLevelName
                    };
                    itemsData.items.Add(data);
                }
            }

            string json = JsonUtility.ToJson(itemsData);
            //print($"Saving Items: {json}");
            PlayerPrefs.SetString("levelItemsData", json);
        }
    }

    public void LoadItems(string currentLevelName)
    {
        if (PlayerPrefs.HasKey("levelItemsData") && PlayerPrefs.GetString("levelItemsData") != "")
        {
            string json = PlayerPrefs.GetString("levelItemsData");
            //print($"loading items: {json}");
            LevelItemsData itemsData = JsonUtility.FromJson<LevelItemsData>(json);

            // Gehe durch die Items und setze ihren Zustand
            GameObject itemsParent = GameObject.Find("Items");
            foreach (ItemData data in itemsData.items)
            {
                if (data.levelName == currentLevelName)
                {
                    Transform item = itemsParent.transform.Find(data.itemName);
                    if (item != null)
                    {
                        //print($"{item.name} set to {data.isActive}");
                        item.gameObject.SetActive(data.isActive);
                    }
                }
            }
        }
        else
        {
            Debug.Log("no item information found");
        }
    }

    private void OnApplicationQuit()
    {
        SaveItems();
    }
}