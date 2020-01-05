using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class SaveSystem
{
    private const string SAVE_FOLDER = "/save";
    private const string PLAYER_SAVE_FILE = "/player.dat";
    private const string INVENTORY_SAVE_FILE = "/inventory.dat";
    private const string EQUIPMENT_SAVE_FILE = "/equipment.dat";
    private static BinaryFormatter formatter = new BinaryFormatter();

    public static bool IsSaveFileExists()
    {
        return Directory.Exists(Application.persistentDataPath + SAVE_FOLDER);
    }

    public static void LoadGame()
    {
        PlayerSingleton.instance.LoadPlayer(LoadPlayerData());
        InventoryManager.instance.LoadInventory(LoadInventoryData());
        EquipmentManager.instance.LoadEquipment(LoadEquipmentData());
    }

    public static void DeleteSavedGame()
    {
        if (IsSaveFileExists())
        {
            Directory.Delete(Application.persistentDataPath + SAVE_FOLDER, true);
            ContractManager.instance.ResetAllContractFlags();
        }
        Debug.Log("Save files deleted");
    }

    public static void SavePlayerData(PlayerSingleton player)
    {
        if (!IsSaveFileExists())
        {
            Directory.CreateDirectory(Application.persistentDataPath + SAVE_FOLDER);
        }

        formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + SAVE_FOLDER + PLAYER_SAVE_FILE;
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

    public static PlayerData LoadPlayerData()
    {
        PlayerData data;
        string path = Application.persistentDataPath + SAVE_FOLDER + PLAYER_SAVE_FILE;
        if (File.Exists(path))
        {
            formatter = new BinaryFormatter();
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

    public static void SaveInventoryData(InventoryManager manager)
    {
        if (!IsSaveFileExists())
        {
            Directory.CreateDirectory(Application.persistentDataPath + SAVE_FOLDER);
        }

        formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + SAVE_FOLDER + INVENTORY_SAVE_FILE;
        FileStream stream = new FileStream(path, FileMode.Create);
        try
        {
            InventoryData data = new InventoryData(manager);
            formatter.Serialize(stream, data);
        }
        finally
        {
            stream.Close();
        }
        Debug.Log("Inventory saved.");
    }

    public static InventoryData LoadInventoryData()
    {
        InventoryData data;
        string path = Application.persistentDataPath + SAVE_FOLDER + INVENTORY_SAVE_FILE;
        if (File.Exists(path))
        {
            formatter = new BinaryFormatter();
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

    public static void SaveEquipmentData(EquipmentManager manager)
    {
        if (!IsSaveFileExists())
        {
            Directory.CreateDirectory(Application.persistentDataPath + SAVE_FOLDER);
        }

        formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + SAVE_FOLDER + EQUIPMENT_SAVE_FILE;
        FileStream stream = new FileStream(path, FileMode.Create);
        try
        {
            EquipmentData data = new EquipmentData(manager);
            formatter.Serialize(stream, data);
        }
        finally
        {
            stream.Close();
        }
        Debug.Log("Equipment saved.");
    }

    public static EquipmentData LoadEquipmentData()
    {
        EquipmentData data;
        string path = Application.persistentDataPath + SAVE_FOLDER + EQUIPMENT_SAVE_FILE;
        if (File.Exists(path))
        {
            formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            try
            {
                data = formatter.Deserialize(stream) as EquipmentData;
            }
            finally
            {
                stream.Close();
            }
            Debug.Log("Equipment loaded.");
            return data;
        }
        else
        {
            Debug.Log($"Save file not found in {path}");
            return null;
        }
    }

    public static void CreateNewSave()
    {
        DeleteSavedGame();

        formatter = new BinaryFormatter();
        Directory.CreateDirectory(Application.persistentDataPath + SAVE_FOLDER);

        string path = Application.persistentDataPath + SAVE_FOLDER + PLAYER_SAVE_FILE;
        FileStream stream = new FileStream(path, FileMode.CreateNew);
        try
        {
            PlayerData data = new PlayerData(PlayerSingleton.instance);
            formatter.Serialize(stream, data);
        }
        finally
        {
            stream.Close();
        }

        path = Application.persistentDataPath + SAVE_FOLDER + INVENTORY_SAVE_FILE;
        stream = new FileStream(path, FileMode.CreateNew);
        try
        {
            InventoryData data = new InventoryData(InventoryManager.instance);
            formatter.Serialize(stream, data);
        }
        finally
        {
            stream.Close();
        }

        path = Application.persistentDataPath + SAVE_FOLDER + EQUIPMENT_SAVE_FILE;
        stream = new FileStream(path, FileMode.CreateNew);
        try
        {
            EquipmentData data = new EquipmentData(EquipmentManager.instance);
            formatter.Serialize(stream, data);
        }
        finally
        {
            stream.Close();
        }
    }
}
