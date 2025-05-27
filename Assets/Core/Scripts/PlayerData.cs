using System.Collections.Generic;

[System.Serializable]
public class PlayerData
{
    public string playerName = "Новый игрок";
    public int playerLevel = 1;
    public int experience = 0;
    public int coins = 100;
    public int gems = 10;
    public int lastCompletedLevel = 0;

    public List<PlayerResource> resources = new List<PlayerResource>();
    public List<PlayerCharacter> characters = new List<PlayerCharacter>();
    public GameSettings settings = new GameSettings();

    // Метод для добавления ресурсов
    public void AddResource(int resourceId, int amount)
    {
        var resource = resources.Find(r => r.id == resourceId);
        if (resource != null)
        {
            resource.amount += amount;
        }
        else
        {
            resources.Add(new PlayerResource { id = resourceId, amount = amount });
        }
    }
}
