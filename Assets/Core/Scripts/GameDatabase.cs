using System.Collections.Generic;

[System.Serializable]
public class GameDatabase
{
    public List<CharacterData> characters = new List<CharacterData>();
    public List<ResourceData> resources = new List<ResourceData>();

    // ����� ��� ��������� ��������� �� ID
    public CharacterData GetCharacterById(int id)
    {
        return characters.Find(c => c.id == id);
    }

    // ����� ��� ��������� ������� �� ID
    public ResourceData GetResourceById(int id)
    {
        return resources.Find(r => r.id == id);
    }
}