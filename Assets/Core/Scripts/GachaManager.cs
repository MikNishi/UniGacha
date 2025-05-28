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
            Debug.Log("������������ ���������� ��� ������");
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
        // ���� ���������� 100 ������ ��� ��������� � ��� ��������� ��������������
        if (player.pullsSinceLastCharacter >= 9)
        {
            player.pullsSinceLastCharacter = 0;
            int guaranteedCharacterId = 1; // ����� ��������� ��� ������������� ID
            return new GachaResult { resultType = "��������-������", reward = guaranteedCharacterId.ToString() };
        }

        float roll = Random.Range(0f, 100f);

        if (roll <= 3f)
        {
            player.pullsSinceLastCharacter = 0; // �������� �������
            int characterId = 1;
            return new GachaResult { resultType = "��������-������", reward = characterId.ToString() };
        }
        else if (roll <= 18f)
        {
            player.gems += 80;
            player.pullsSinceLastCharacter++; // +1 � ��������
            return new GachaResult { resultType = "������", reward = "+80 ����������" };
        }
        else if (roll <= 33f)
        {
            player.pullsSinceLastCharacter++; // +1 � ��������
            return new GachaResult { resultType = "������", reward = "������ �� ������" };
        }
        else
        {

            float subRoll = Random.Range(0f, 100f);
            player.pullsSinceLastCharacter++; // +1 � ��������

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
        if (result.resultType == "��������-������")
        {
            int characterId = int.Parse(result.reward);
            player.AddCharacterPull(characterId);
            Debug.Log($"����� �������� � ID {characterId}. ��������/�������� ��������. - ������");
        }
        if (result.resultType == "��������-������")
        {
            int characterId = int.Parse(result.reward);
            player.AddCharacterPull(characterId);
            Debug.Log($"����� �������� � ID {characterId}. ��������/�������� ��������. - ������");
        }
        else
        {
            Debug.Log($"������: {result.resultType} � {result.reward}");
        }
    }

}
