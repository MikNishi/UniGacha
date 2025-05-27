using UnityEngine;
using System.IO; // ��������� ������������ ����

public class SaveSystemInitializer : MonoBehaviour
{
    private void Start()
    {
        InitializeSaveSystem();
    }

    private void InitializeSaveSystem()
    {
        // 1. ������������� ������� ������ (data.json)
        if (!File.Exists(SaveManager.DataPath))
        {
            TextAsset jsonFile = Resources.Load<TextAsset>("data");
            if (jsonFile != null)
            {
                File.WriteAllText(SaveManager.DataPath, jsonFile.text);
                Debug.Log("data.json ������ �: " + SaveManager.DataPath);
            }
            else
            {
                Debug.LogError("�� ������ data.json � Resources!");
                // ������� ������ ���� ������, ���� ���� �� ������
                GameDatabase defaultData = new GameDatabase
                {
                    characters = new System.Collections.Generic.List<CharacterData>(),
                    resources = new System.Collections.Generic.List<ResourceData>()
                };
                SaveManager.SaveGameData(defaultData);
            }
        }

        // 2. ������������� ������ ������ (player.json)
        if (!File.Exists(SaveManager.PlayerPath))
        {
            PlayerData newPlayer = new PlayerData
            {
                playerName = "",
                playerLevel = 1,
                coins = 100 // ��������� ���������� ������
            };
            SaveManager.SavePlayerData(newPlayer);
            Debug.Log("player.json ������ �: " + SaveManager.PlayerPath);
        }
    }
}