[System.Serializable]
public class PassiveBonus
{
    public PassiveType type;
    public float value;
}

public enum PassiveType
{
    GoldBonus,
    ResearchSpeed,
    ProgressSpeed
}
