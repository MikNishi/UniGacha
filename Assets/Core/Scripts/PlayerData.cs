using System.Collections.Generic;

[System.Serializable]
public class CharacterPullData
{
    public int characterId;
    public int pullCount;
}

[System.Serializable]
public class PlayerData
{
    public string playerName = "Новый игрок";
    public int playerLevel = 1;
    public int experience = 0;
    public int coins = 100;

    public int gems = 16000;
    public int tickets = 100;
    public int pullsSinceLastCharacter = 0;

    public int lastCompletedLevel = 0;

    public List<PlayerResource> resources = new List<PlayerResource>();
    public List<PlayerCharacter> characters = new List<PlayerCharacter>();
    public GameSettings settings = new GameSettings();

    public List<CharacterPullData> pulledCharacters
        = new List<CharacterPullData>();

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

    //Метод для добавления или увеличения счётчика
    public void AddCharacterPull(int characterId)
    {
        var data = pulledCharacters.Find(c => c.characterId == characterId);
        if (data != null)
        {
            data.pullCount++;
        }
        else
        {
            pulledCharacters.Add(new CharacterPullData
            {
                characterId = characterId,
                pullCount = 1
            });
        }
    }


}
