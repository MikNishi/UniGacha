using System.IO;
using UnityEngine;
using System.Collections.Generic;

public static class SaveManager
{
    public static string DataPath => Path.Combine(Application.persistentDataPath, "data.json");
    public static string PlayerPath => Path.Combine(Application.persistentDataPath, "player.json");
    public static string DepartmentsPath => Path.Combine(Application.persistentDataPath, "departments.json");
    public static string CharactersPath => Path.Combine(Application.persistentDataPath, "characters.json");

    // --- GameDatabase ---
    public static GameDatabase LoadGameData()
    {
        if (!File.Exists(DataPath))
        {
            Debug.LogWarning($"Game data not found at: {DataPath}");
            return new GameDatabase(); // Возвращаем пустой объект вместо null
        }

        try
        {
            string json = File.ReadAllText(DataPath);
            return JsonUtility.FromJson<GameDatabase>(json) ?? new GameDatabase();
        }
        catch (System.Exception ex)
        {
            Debug.LogError($"Failed to load GameData: {ex.Message}");
            return new GameDatabase();
        }
    }

    public static void SaveGameData(GameDatabase data)
    {
        if (data == null)
        {
            Debug.LogWarning("Trying to save null GameDatabase");
            return;
        }

        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(DataPath, json);
    }

    // --- PlayerData ---
    public static PlayerData LoadPlayerData()
    {
        if (!File.Exists(PlayerPath))
        {
            Debug.Log("Player data not found, returning new PlayerData");
            return new PlayerData();
        }

        try
        {
            string json = File.ReadAllText(PlayerPath);
            return JsonUtility.FromJson<PlayerData>(json) ?? new PlayerData();
        }
        catch (System.Exception ex)
        {
            Debug.LogError($"Failed to load PlayerData: {ex.Message}");
            return new PlayerData();
        }
    }

    public static void SavePlayerData(PlayerData data)
    {
        if (data == null)
        {
            Debug.LogWarning("Trying to save null PlayerData");
            return;
        }

        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(PlayerPath, json);
    }

    // --- Departments ---
    public static void SaveDepartments(List<Department> departments)
    {
        if (departments == null)
        {
            Debug.LogWarning("Trying to save null Department list");
            return;
        }

        DepartmentListWrapper wrapper = new DepartmentListWrapper { departments = departments };
        string json = JsonUtility.ToJson(wrapper, true);
        File.WriteAllText(DepartmentsPath, json);
    }

    public static List<Department> LoadDepartments()
    {
        if (!File.Exists(DepartmentsPath))
        {
            Debug.Log("Departments file not found, returning empty list");
            return new List<Department>();
        }

        try
        {
            string json = File.ReadAllText(DepartmentsPath);
            DepartmentListWrapper wrapper = JsonUtility.FromJson<DepartmentListWrapper>(json);
            return wrapper?.departments ?? new List<Department>();
        }
        catch (System.Exception ex)
        {
            Debug.LogError($"Failed to load Departments: {ex.Message}");
            return new List<Department>();
        }
    }

    // --- Characters ---
    public static void SaveCharacters(List<CharacterData> characters)
    {
        if (characters == null)
        {
            Debug.LogWarning("Trying to save null Character list");
            return;
        }

        CharacterListWrapper wrapper = new CharacterListWrapper { characters = characters };
        string json = JsonUtility.ToJson(wrapper, true);
        File.WriteAllText(CharactersPath, json);
    }

    public static List<CharacterData> LoadCharacters()
    {
        if (!File.Exists(CharactersPath))
        {
            Debug.Log("Characters file not found, returning empty list");
            return new List<CharacterData>();
        }

        try
        {
            string json = File.ReadAllText(CharactersPath);
            CharacterListWrapper wrapper = JsonUtility.FromJson<CharacterListWrapper>(json);
            return wrapper?.characters ?? new List<CharacterData>();
        }
        catch (System.Exception ex)
        {
            Debug.LogError($"Failed to load Characters: {ex.Message}");
            return new List<CharacterData>();
        }
    }

    // --- Удаление всех сохранений ---
    public static void DeleteAllSaves()
    {
        TryDeleteFile(PlayerPath);
        TryDeleteFile(DataPath);
        TryDeleteFile(DepartmentsPath);
        TryDeleteFile(CharactersPath);

        PlayerPrefs.DeleteAll();
        Debug.Log("All saves and PlayerPrefs deleted");
    }

    private static void TryDeleteFile(string path)
    {
        try
        {
            if (File.Exists(path))
                File.Delete(path);
        }
        catch (System.Exception ex)
        {
            Debug.LogError($"Failed to delete file at {path}: {ex.Message}");
        }
    }

    // --- Обёртки для сериализации списков ---
    [System.Serializable]
    private class DepartmentListWrapper
    {
        public List<Department> departments;
    }

    [System.Serializable]
    private class CharacterListWrapper
    {
        public List<CharacterData> characters;
    }
}
