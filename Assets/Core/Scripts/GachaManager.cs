using UnityEngine;
using UnityEngine.UI;
using TMPro;
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
    public GameObject resultPanel;
    public Image resultIcon;
    public TMP_Text resultText;
    
    private void Start()
    {
        player = SaveManager.LoadPlayerData();
    }

    private bool isResultVisible = false;

    private void ShowResult(string rewardText, Sprite iconSprite)
    {
        resultPanel.SetActive(true);
        resultText.text = rewardText;
        resultIcon.sprite = iconSprite;
        isResultVisible = true;
    }

    public void HideResultPanel()
    {
        resultPanel.SetActive(false);
        isResultVisible = false;
    }

    public void PullOnce()
    {
        if (isResultVisible)
        {
            Debug.Log("Сначала закрой панель результата.");
            return;
        }

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
        if (isResultVisible)
        {
            Debug.Log("Сначала закрой панель результата.");
            return;
        }

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
            Debug.Log("Я выпаллллллл!!!!!!");
            player.pullsSinceLastCharacter = 0; // сбросить счётчик
            int characterId = 1;
            return new GachaResult { resultType = "Персонаж", reward = characterId.ToString() };
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
        if (result.resultType == "Персонаж")
        {
            int characterId = int.Parse(result.reward);
            player.AddCharacterPull(characterId);
            Sprite characterSprite = Resources.Load<Sprite>("Icons/Kotofei");
            ShowResult($"Выпал персонаж #{characterId}", characterSprite);  
        }
        else if (result.resultType == "Кэшбек")
        {
            Sprite gemIcon = Resources.Load<Sprite>("Icons/gem");
            ShowResult(result.reward, gemIcon);
        }
        else if (result.resultType == "Голда")
        {
            Sprite coinIcon = Resources.Load<Sprite>("Icons/coin");
            ShowResult(result.reward, coinIcon);
        }
        else
        {
            Sprite failIcon = Resources.Load<Sprite>("Icons/fail");
            ShowResult("Увы, попробуй еще раз", failIcon);
        }
    }


}
