using System.Collections.Generic;
using UnityEngine.TextCore.Text;

[System.Serializable]
public class Department
{
    public string id;
    public string name;
    public int level;
    public float progress; // 0.0 - 1.0
    public int upgradeCost;
    public List<string> rooms;
    public int assignedCharacterId;
    public List<PassiveBonus> passives;

    public void AssignCharacter(CharacterData character)
    {
        assignedCharacterId = character.id;
        passives = new List<PassiveBonus>(character.passives);
    }

    public void Upgrade()
    {
        level++;
        progress = 0f;
        upgradeCost = CalculateNextCost();
    }

    public void TickProgress(float deltaTime)
    {
        progress += deltaTime * GetAutoProgressRate();
        if (progress >= 1f)
        {
            Upgrade();
        }
    }

    private float GetAutoProgressRate()
    {
        // ћожно учитывать пассивки и бонусы
        float baseRate = 0.01f;
        foreach (var p in passives)
        {
            if (p.type == PassiveType.ProgressSpeed)
                baseRate += p.value;
        }
        return baseRate;
    }

    private int CalculateNextCost()
    {
        return upgradeCost + 500 * level; // пример
    }
}
