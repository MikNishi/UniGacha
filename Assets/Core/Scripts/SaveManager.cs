using System.IO; // ���������, ��� ��� ����
using UnityEngine;

public static class SaveManager
{
    // ���� ����������
    public static string DataPath => Path.Combine(Application.persistentDataPath, "data.json");
    public static string PlayerPath => Path.Combine(Application.persistentDataPath, "player.json");

    // �������� ������� ������
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

    // �������� ������ ������
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

    // ���������� ������ ������
    public static void SavePlayerData(PlayerData data)
    {
        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(PlayerPath, json);
    }

    // ���������� ������� ������
    public static void SaveGameData(GameDatabase data)
    {
        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(DataPath, json);
    }

    // �������� ����������
    public static void DeleteAllSaves()
    {
        if (File.Exists(PlayerPath)) File.Delete(PlayerPath);
        if (File.Exists(DataPath)) File.Delete(DataPath);
        PlayerPrefs.DeleteAll();
        Debug.Log("All saves and prefs deleted");
    }
}