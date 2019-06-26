using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEditor;
using UnityEngine;

public static class SaveSystem
{
    const string SAVE_FOLDER = "/save";

    private static bool IsSaveFileExists()
    {
        return Directory.Exists(Application.persistentDataPath + SAVE_FOLDER);
    }

    [MenuItem("Save System/Delete Save")]
    private static void DeleteSaveFolder()
    {
        if (IsSaveFileExists())
        {
            Directory.Delete(Application.persistentDataPath + SAVE_FOLDER, true);
        }
        Debug.Log("Save files deleted");
    }

    public static void SavePlayer(PlayerSingleton player)
    {
        if (!IsSaveFileExists())
        {
            Directory.CreateDirectory(Application.persistentDataPath + SAVE_FOLDER);
        }

        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + SAVE_FOLDER + "/player.data";
        FileStream stream = new FileStream(path, FileMode.Create);
        try
        {
            PlayerData data = new PlayerData(player);
            formatter.Serialize(stream, data);
        }
        finally
        {
            stream.Close();
        }
    }

    public static PlayerData LoadPlayer()
    {
        PlayerData data;
        string path = Application.persistentDataPath + SAVE_FOLDER + "/player.data";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            try
            {
                data = formatter.Deserialize(stream) as PlayerData;
            }
            finally
            {
                stream.Close();
            }

            return data;
        }
        else
        {
            Debug.Log($"Save file not found in {path}");
            return null;
        }
    }

    [MenuItem("Save System/Save Inventory")]
    public static void SaveInventory(InventoryManager inventory)
    {
        if (!IsSaveFileExists())
        {
            Directory.CreateDirectory(Application.persistentDataPath + SAVE_FOLDER);
        }

        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + SAVE_FOLDER + "/inventory.data";
        FileStream stream = new FileStream(path, FileMode.Create);
        try
        {
            InventoryData data = new InventoryData(inventory);
            formatter.Serialize(stream, data);
        }
        finally
        {
            stream.Close();
        }
        Debug.Log("Inventory saved.");
    }

    [MenuItem("Save System/Load Inventory")]
    public static InventoryData LoadInventory()
    {
        InventoryData data;
        string path = Application.persistentDataPath + SAVE_FOLDER + "/inventory.data";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            try
            {
                data = formatter.Deserialize(stream) as InventoryData;
            }
            finally
            {
                stream.Close();
            }
            Debug.Log("Inventory loaded.");
            return data;
        }
        else
        {
            Debug.Log($"Save file not found in {path}");
            return null;
        }
    }

}
