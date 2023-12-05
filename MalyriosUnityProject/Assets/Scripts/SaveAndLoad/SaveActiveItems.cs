using System;
using System.Collections.Generic;
using System.IO;
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
    private string saveFilePath;

    private void Start()
    {
        levelManager = ReferencesManager.Instance.levelManager;
        saveFilePath = Path.Combine(Application.persistentDataPath, "levelItemsData.json");
        LoadItems(levelManager.GetCurrentLevelName());
    }

    public void SaveItems()
    {
        if (itemsData.items == null)
        {
            itemsData.items = new List<ItemData>();
        }
        GameObject itemsParent = GameObject.Find("Items");
        if (itemsParent)
        {
            foreach (Transform item in itemsParent.transform)
            {
                string itemName = item.name;
                string currentLevelName = levelManager.GetCurrentLevelName();

                itemsData.items.RemoveAll(i => i.itemName == itemName && i.levelName == currentLevelName);

                ItemData data = new ItemData
                {
                    itemName = itemName,
                    isActive = item.gameObject.activeSelf,
                    levelName = currentLevelName
                };
                itemsData.items.Add(data);
            }

            string json = JsonUtility.ToJson(itemsData);
            File.WriteAllText(saveFilePath, json);
        }
    }

    public void LoadItems(string currentLevelName)
    {
        if (File.Exists(saveFilePath))
        {
            string json = File.ReadAllText(saveFilePath);
            LevelItemsData loadedItemsData = JsonUtility.FromJson<LevelItemsData>(json);

            GameObject itemsParent = GameObject.Find("Items");
            if (itemsParent)
            {
                foreach (ItemData data in loadedItemsData.items)
                {
                    if (data.levelName == currentLevelName)
                    {
                        Transform item = itemsParent.transform.Find(data.itemName);
                        if (item != null)
                        {
                            item.gameObject.SetActive(data.isActive);
                        }
                    }
                }
            }
        }
        else
        {
            Debug.Log("No item information found");
        }
    }

    private void OnApplicationQuit()
    {
        SaveItems();
    }
}
