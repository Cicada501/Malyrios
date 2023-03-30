using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Malyrios.Items;
using SaveAndLoad;

//static -> can not be instantiated. Dont want multiple versions of it
public static class SaveSystem
{
    //Player
    public static void SavePlayer(Player player)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/player.mydata";
        FileStream stream = new FileStream(path, FileMode.Create);

        PlayerData data = new PlayerData(player);

        //write Data to file
        formatter.Serialize(stream, data);
        stream.Close();
    }
    
    

    public static PlayerData LoadPlayer()
    {
        string path = Application.persistentDataPath + "/player.mydata";
        BinaryFormatter formatter = new BinaryFormatter();
        if (!File.Exists(path))
        {
            Debug.LogError("Playerdata Save file not found in" + path);
            Player player = GameObject.Find("Player").GetComponent<Player>();
            SavePlayer(player);
        }

        var stream = new FileStream(path, FileMode.Open);
        PlayerData data;
        if (stream.Length>0)
        {
            data = formatter.Deserialize(stream) as PlayerData;  
        }
        else
        {
            Player playerData = GameObject.Find("Player").GetComponent<Player>();
            data = new PlayerData(playerData);
        }
        
        stream.Close();
        
        

        return data;
    }

    //Inventory
    public static void SaveInventory(Inventory inventory)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/inventory.mydata";
        FileStream stream = new FileStream(path, FileMode.Create);
        InventoryData data = new InventoryData(inventory);
        //write Data to file
        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static InventoryData LoadInventory()
    {
        string path = Application.persistentDataPath + "/inventory.mydata";
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream;
        if (!File.Exists(path))
        {
            Debug.LogError("Inventory Save file not found in (but created one now)" + path);
            Inventory inventory = GameObject.Find("Player").GetComponent<Inventory>();
            
            SaveInventory(inventory);
        }

        stream = new FileStream(path, FileMode.Open);
        InventoryData data;
        if (stream.Length>0)
        {
          data = formatter.Deserialize(stream) as InventoryData;  
        }
        else
        {
            Inventory inventory = GameObject.Find("Player").GetComponent<Inventory>();
            data = new InventoryData(inventory);
        }
        
        stream.Close();

        return data;
    }
    
    //Decisions
    public static void SaveDecisions(Decision decision)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/decision.mydata";
        FileStream stream = new FileStream(path, FileMode.Create);

        DecisionData data = new DecisionData(); //directly takes the Global decision values from the Decision 
        //write Data to file
        formatter.Serialize(stream, data);
        stream.Close();
    }
    
    public static DecisionData LoadDecisions()
    {
        string path = Application.persistentDataPath + "/decision.mydata";
        BinaryFormatter formatter = new BinaryFormatter();
        if (!File.Exists(path))
        {
            Debug.LogError("Decision Save file not found in" + path+ "(but created one now)");
            Decision decision = GameObject.Find("GameManager").GetComponent<Decision>();
            SaveDecisions(decision);
        }

        var stream = new FileStream(path, FileMode.Open);
        DecisionData data = formatter.Deserialize(stream) as DecisionData;
        stream.Close();

        return data;
    }
}