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

    public TMP_Text gemsText;      
    public TMP_Text ticketsText;   

    private bool isResultVisible = false;
    private Queue<GachaResult> resultQueue = new Queue<GachaResult>();

    private void Start()
    {
        player = SaveManager.LoadPlayerData();
        UpdateCurrencyDisplay();
    }

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
        ShowNextResultInQueue(); // ���������� ��������� ���������, ���� ����
    }

    public void PullOnce()
    {
        if (isResultVisible)
        {
            Debug.Log("������� ������ ������ ����������.");
            return;
        }

        if (player.tickets > 0 || player.gems >= 160)
        {
            SpendCurrency();
            GachaResult result = RollGacha();
            ApplyResult(result);
            SaveManager.SavePlayerData(player);
            UpdateCurrencyDisplay();
        }
        else
        {
            Debug.Log("������������ ���������� ��� ������");
        }
    }

    public void PullTen()
    {
        if (isResultVisible)
        {
            Debug.Log("������� ������ ������ ����������.");
            return;
        }

        for (int i = 0; i < 10; i++)
        {
            if (player.tickets > 0 || player.gems >= 160)
            {
                SpendCurrency();
                GachaResult result = RollGacha();
                resultQueue.Enqueue(result);
            }
            else
            {
                Debug.Log("������������ ������ ��� 10 ������.");
                break;
            }
        }

        SaveManager.SavePlayerData(player);
        UpdateCurrencyDisplay();
        ShowNextResultInQueue(); // �������� �����
    }

    private void ShowNextResultInQueue()
    {
        if (resultQueue.Count > 0)
        {
            GachaResult nextResult = resultQueue.Dequeue();
            ApplyResult(nextResult);
        }
        else
        {
            Debug.Log("��� ���������� ��������.");
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

    private void UpdateCurrencyDisplay()
    {
        gemsText.text = $"���������: {player.gems}";
        ticketsText.text = $"������: {player.tickets}";
    }

    private GachaResult RollGacha()
    {
        if (player.pullsSinceLastCharacter >= 9)
        {
            player.pullsSinceLastCharacter = 0;
            int guaranteedCharacterId = 1;
            return new GachaResult { resultType = "��������-������", reward = guaranteedCharacterId.ToString() };
        }

        float roll = Random.Range(0f, 100f);

        if (roll <= 3f)
        {
            Debug.Log("� �����������!!!!!!");
            player.pullsSinceLastCharacter = 0;
            int characterId = 1;
            return new GachaResult { resultType = "��������", reward = characterId.ToString() };
        }
        else if (roll <= 18f)
        {
            player.gems += 80;
            player.pullsSinceLastCharacter++;
            return new GachaResult { resultType = "������", reward = "+80 ����������" };
        }
        else if (roll <= 33f)
        {
            player.pullsSinceLastCharacter++;
            return new GachaResult { resultType = "������", reward = "������ �� ������" };
        }
        else
        {
            float subRoll = Random.Range(0f, 100f);
            player.pullsSinceLastCharacter++;

            if (subRoll < 60f)
            {
                player.coins += 100;
                return new GachaResult { resultType = "�����", reward = "100 �����" };
            }
            else if (subRoll < 90f)
            {
                player.coins += 1000;
                return new GachaResult { resultType = "�����", reward = "1000 �����" };
            }
            else
            {
                player.coins += 10000;
                return new GachaResult { resultType = "�����", reward = "10000 �����" };
            }
        }
    }

    private void ApplyResult(GachaResult result)
    {
        if (result.resultType == "��������" || result.resultType == "��������-������")
        {
            int characterId = int.Parse(result.reward);
            player.AddCharacterPull(characterId);
            Sprite characterSprite = Resources.Load<Sprite>("Icons/Kotofei");
            ShowResult($"����� �������� #{characterId}", characterSprite);
        }
        else if (result.resultType == "������")
        {
            Sprite gemIcon = Resources.Load<Sprite>("Icons/gem");
            ShowResult(result.reward, gemIcon);
        }
        else if (result.resultType == "�����")
        {
            Sprite coinIcon = Resources.Load<Sprite>("Icons/coin");
            ShowResult(result.reward, coinIcon);
        }
        else
        {
            Sprite failIcon = Resources.Load<Sprite>("Icons/fail");
            ShowResult("���, �������� ��� ���", failIcon);
        }
    }
}
