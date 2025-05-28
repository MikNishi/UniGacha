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
            Debug.Log("������������ ���������� ��� ������");
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
                Debug.Log("�� 10");
            }
            else
            {
                Debug.Log("�������� ������ 10 ������/����������");
                break;
            }
        }

        SaveManager.SavePlayerData(player);
        foreach (var res in results)
        {
            Debug.Log("���������: " + res.resultType + " - " + res.reward);
        }
    }

    private int GetTickets()
    {
        var ticket = player.resources.Find(r => r.id == 1); // id = 1 ��� ������
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
            return new GachaResult { resultType = "��������", reward = "����� ��������!" };
        }
        else if (roll <= 18f)
        {
            player.gems += 80; // ������ �������� ���������
            return new GachaResult { resultType = "������", reward = "+80 ����������" };
        }
        else if (roll <= 33f)
        {
            return new GachaResult { resultType = "������", reward = "������ �� ������" };
        }
        else
        {
            // ��������� 67%: 100 / 1000 / 10000 ������
            float subRoll = Random.Range(0f, 100f);
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
        // ����� ����� ����������� ���������� ��������� � ������, ����� �� ����� � �.�.
        Debug.Log($"������: {result.resultType} � {result.reward}");
    }
}
