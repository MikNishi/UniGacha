using UnityEngine;
using System.IO; // Добавляем пространство имен

public class SaveSystemInitializer : MonoBehaviour
{
    private void Start()
    {
        InitializeSaveSystem();
    }

    private void InitializeSaveSystem()
    {
        // 1. Инициализация игровых данных (data.json)
        if (!File.Exists(SaveManager.DataPath))
        {
            TextAsset jsonFile = Resources.Load<TextAsset>("data");
            if (jsonFile != null)
            {
                File.WriteAllText(SaveManager.DataPath, jsonFile.text);
                Debug.Log("data.json создан в: " + SaveManager.DataPath);
            }
            else
            {
                Debug.LogError("Не найден data.json в Resources!");
                // Создаем пустую базу данных, если файл не найден
                GameDatabase defaultData = new GameDatabase
                {
                    characters = new System.Collections.Generic.List<CharacterData>(),
                    resources = new System.Collections.Generic.List<ResourceData>()
                };
                SaveManager.SaveGameData(defaultData);
            }
        }

        // 2. Инициализация данных игрока (player.json)
        if (!File.Exists(SaveManager.PlayerPath))
        {
            PlayerData newPlayer = new PlayerData
            {
                playerName = "",
                playerLevel = 1,
                coins = 100 // Стартовое количество валюты
            };
            SaveManager.SavePlayerData(newPlayer);
            Debug.Log("player.json создан в: " + SaveManager.PlayerPath);
        }
    }
}