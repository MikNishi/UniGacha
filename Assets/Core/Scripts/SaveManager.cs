using System.IO; // Убедитесь, что это есть
using UnityEngine;

public static class SaveManager
{
    // Пути сохранения
    public static string DataPath => Path.Combine(Application.persistentDataPath, "data.json");
    public static string PlayerPath => Path.Combine(Application.persistentDataPath, "player.json");

    // Загрузка игровых данных
    public static GameDatabase LoadGameData()
    {
        if (!File.Exists(DataPath))
        {
            Debug.LogError("Game data file not found at: " + DataPath);
            return null;
        }

        string json = File.ReadAllText(DataPath);
        return JsonUtility.FromJson<GameDatabase>(json);
    }

    // Загрузка данных игрока
    public static PlayerData LoadPlayerData()
    {
        if (!File.Exists(PlayerPath))
        {
            Debug.Log("Player data not found, returning new player");
            return new PlayerData();
        }

        string json = File.ReadAllText(PlayerPath);
        return JsonUtility.FromJson<PlayerData>(json);
    }

    // Сохранение данных игрока
    public static void SavePlayerData(PlayerData data)
    {
        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(PlayerPath, json);
    }

    // Сохранение игровых данных
    public static void SaveGameData(GameDatabase data)
    {
        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(DataPath, json);
    }

    // Удаление сохранений
    public static void DeleteAllSaves()
    {
        if (File.Exists(PlayerPath)) File.Delete(PlayerPath);
        if (File.Exists(DataPath)) File.Delete(DataPath);
        PlayerPrefs.DeleteAll();
        Debug.Log("All saves and prefs deleted");
    }
}