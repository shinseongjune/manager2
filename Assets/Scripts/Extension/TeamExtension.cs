public static class TeamExtension
{
    public static void AddPlayer(this Team team, ref Player player)
    {
        if (!team.Players.Contains(player.idNumber) && GameManager.Instance.Players.ContainsKey(player.idNumber))
        {
            team.Players.Add(player.idNumber);
            player.team = team.idNumber;
        }
    }

    public static void AddPlayer(this Team team, int playerId)
    {
        if (!team.Players.Contains(playerId) && GameManager.Instance.Players.ContainsKey(playerId))
        {
            team.Players.Add(playerId);
            GameManager.Instance.Players[playerId].team = team.idNumber;
        }
    }


    public static void DeletePlayer(this Team team, ref Player player)
    {
        if (team.Players.Contains(player.idNumber) && GameManager.Instance.Players.ContainsKey(player.idNumber))
        {
            team.Players.Remove(player.idNumber);
            player.team = -1;
        }
    }

    public static void DeletePlayer(this Team team, int playerId)
    {
        if (team.Players.Contains(playerId) && GameManager.Instance.Players.ContainsKey(playerId))
        {
            team.Players.Remove(playerId);
            GameManager.Instance.Players[playerId].team = -1;
        }
    }
}
