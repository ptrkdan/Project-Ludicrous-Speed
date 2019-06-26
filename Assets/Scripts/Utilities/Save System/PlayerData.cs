[System.Serializable]
public class PlayerData
{
    // Basic Info
    public string playerName;
    public int playerLevel;
    public int experiencePoints;
    public int[] playerPrereqStatus;

    public PlayerData(PlayerSingleton player)
    {
        playerName = player.PlayerName;
        playerLevel = player.PlayerLevel;
        experiencePoints = player.ExperiencePoints;
        playerPrereqStatus = player.GetPrereqStatus().GetCriteria();
    }
}
