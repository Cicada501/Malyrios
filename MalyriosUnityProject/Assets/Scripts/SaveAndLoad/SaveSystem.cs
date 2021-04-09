﻿using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

//static -> can not be instantiated. Dont want multiple versions of it
public static class SaveSystem
{
    //Player
    public static void savePlayer(Player player){
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/player.mydata";
        FileStream stream = new FileStream(path, FileMode.Create);

        PlayerData data = new PlayerData(player);

        //write Data to file
        formatter.Serialize(stream, data);
        stream.Close();
    }
    public static PlayerData LoadPlayer(){
        string path = Application.persistentDataPath + "/player.mydata";
        if(File.Exists(path)){
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            PlayerData data = formatter.Deserialize(stream) as PlayerData;
            stream.Close();

            return data;
        }else
        {
            Debug.LogError("Save file not found in" + path);
            return null;
        }
    }

    //Inventory
    public static void saveInventory(Inventory inventory){
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/inventory.mydata";
        FileStream stream = new FileStream(path, FileMode.Create);

        InventoryData data = new InventoryData(inventory);

        //write Data to file
        formatter.Serialize(stream, data);
        stream.Close();
    }
    
    public static InventoryData LoadInventory(){
        string path = Application.persistentDataPath + "/inventory.mydata";
        if(File.Exists(path)){
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            InventoryData data = formatter.Deserialize(stream) as InventoryData;
            stream.Close();

            return data;
        }else
        {
            Debug.LogError("Save file not found in" + path);
            return null;
        }
    }

}