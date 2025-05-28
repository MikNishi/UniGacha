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
        if (player.gems >= 160 || GetTickets() >= 1)
        {
            SpendCurrency(1);
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
            if (player.gems >= 160 || GetTickets() >= 1)
            {
                SpendCurrency(1);
                GachaResult result = RollGacha();
                ApplyResult(result);
                results.Add(result);
                Debug.Log("ОК 10");
            }
            else
            {
                Debug.Log("Осталось меньше 10 круток/примогемов");
                break;
            }
        }

        SaveManager.SavePlayerData(player);
        foreach (var res in results)
        {
            Debug.Log("Результат: " + res.resultType + " - " + res.reward);
        }
    }

    private int GetTickets()
    {
        var ticket = player.resources.Find(r => r.id == 1); // id = 1 для круток
        return ticket != null ? ticket.amount : 0;
    }

    private void SpendCurrency(int pulls)
    {
        var ticket = player.resources.Find(r => r.id == 1);
        if (ticket != null && ticket.amount >= pulls)
        {
            ticket.amount -= pulls;
        }
        else
        {
            player.gems -= 160 * pulls;
        }
    }

    private GachaResult RollGacha()
    {
        float roll = Random.Range(0f, 100f);

        if (roll <= 30f)
        {
            return new GachaResult { resultType = "Персонаж", reward = "Новый персонаж!" };
        }
        else if (roll <= 18f)
        {
            player.gems += 80; // Кэшбек половины стоимости
            return new GachaResult { resultType = "Кэшбек", reward = "+80 примогемов" };
        }
        else if (roll <= 33f)
        {
            return new GachaResult { resultType = "Провал", reward = "Ничего не выпало" };
        }
        else
        {
            // Остальные 67%: 100 / 1000 / 10000 золота
            float subRoll = Random.Range(0f, 100f);
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
        // Здесь можно реализовать добавление персонажа в список, вывод на экран и т.д.
        Debug.Log($"Выпало: {result.resultType} — {result.reward}");
    }
}
