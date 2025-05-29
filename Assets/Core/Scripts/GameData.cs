using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CharacterData
{
    public int id;
    public string name;
    public int health;
    public int damage;
    public int unlockLevel = 1;
    public bool isUnlocked = false;

    // Новое поле — список пассивок
    public List<PassiveBonus> passives = new List<PassiveBonus>();
}


[System.Serializable]
public class ResourceData
{
    public int id;
    public string name;
    public string iconPath; // Путь к иконке ресурса
}

[System.Serializable]
public class PlayerResource
{
    public int id;
    public int amount;
}

[System.Serializable]
public class PlayerCharacter
{
    public int id;
    public int level = 1;
    public int experience;
}

[System.Serializable]
public class GameSettings
{
    public float musicVolume = 0.7f;
    public float sfxVolume = 0.7f;
    public bool vibrationEnabled = true;
}
