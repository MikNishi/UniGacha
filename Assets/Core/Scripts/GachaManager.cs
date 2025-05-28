using UnityEngine;
using System.Collections.Generic;

public class GachaManager : MonoBehaviour
{
    [System.Serializable]
    public class GachaResult
    {
        public string resultType;
        public string reward;
    }

    private PlayerData player;

    private void Start()
    {
        player = SaveManager.LoadPlayerData();
    }

    public void PullOnce()
    {
        if ( player.tickets > 0 || player.gems >= 160 )
        {
            SpendCurrency();
            GachaResult result = RollGacha();
            ApplyResult(result);
            SaveManager.SavePlayerData(player);
        }
        else
        {
            Debug.Log("Недостаточно примогемов или круток");
        }
    }

    public void PullTen()
    {
        List<GachaResult> results = new List<GachaResult>();

        for (int i = 0; i < 10; i++)
        {
            PullOnce();
        }
    }

    private void SpendCurrency()
    {
        if (player.tickets != 0)
        {
            player.tickets -= 1;
        }
        else
        {
            player.gems -= 160;
        }
    }



    private GachaResult RollGacha()
    {
        // Если накопилось 100 круток без персонажа — даём персонажа гарантированно
        if (player.pullsSinceLastCharacter >= 9)
        {
            player.pullsSinceLastCharacter = 0;
            int guaranteedCharacterId = 1; // Можно случайный или фиксированный ID
            return new GachaResult { resultType = "Персонаж-Гарант", reward = guaranteedCharacterId.ToString() };
        }

        float roll = Random.Range(0f, 100f);

        if (roll <= 3f)
        {
            player.pullsSinceLastCharacter = 0; // сбросить счётчик
            int characterId = 1;
            return new GachaResult { resultType = "Персонаж-Рандом", reward = characterId.ToString() };
        }
        else if (roll <= 18f)
        {
            player.gems += 80;
            player.pullsSinceLastCharacter++; // +1 к счётчику
            return new GachaResult { resultType = "Кэшбек", reward = "+80 примогемов" };
        }
        else if (roll <= 33f)
        {
            player.pullsSinceLastCharacter++; // +1 к счётчику
            return new GachaResult { resultType = "Провал", reward = "Ничего не выпало" };
        }
        else
        {

            float subRoll = Random.Range(0f, 100f);
            player.pullsSinceLastCharacter++; // +1 к счётчику

            if (subRoll < 60f)
            {
                player.coins += 100;
                return new GachaResult { resultType = "Голда", reward = "100 монет" };
            }
            else if (subRoll < 90f)
            {
                player.coins += 1000;
                return new GachaResult { resultType = "Голда", reward = "1000 монет" };
            }
            else
            {
                player.coins += 10000;
                return new GachaResult { resultType = "Голда", reward = "10000 монет" };
            }
        }
    }


    private void ApplyResult(GachaResult result)
    {
        if (result.resultType == "Персонаж-Рандом")
        {
            int characterId = int.Parse(result.reward);
            player.AddCharacterPull(characterId);
            Debug.Log($"Выпал персонаж с ID {characterId}. Добавлен/увеличен дубликат. - Рандом");
        }
        if (result.resultType == "Персонаж-Гарант")
        {
            int characterId = int.Parse(result.reward);
            player.AddCharacterPull(characterId);
            Debug.Log($"Выпал персонаж с ID {characterId}. Добавлен/увеличен дубликат. - Гарант");
        }
        else
        {
            Debug.Log($"Выпало: {result.resultType} — {result.reward}");
        }
    }

}
