public static class TeamExtension
{
    static void AddPlayer(this Team team, ref Player player)
    {
        if (!team.Players.Contains(player.idNumber))
        {
            team.Players.Add(player.idNumber);
            player.team = team.idNumber;
        }
    }

    static void AddPlayer(this Team team, int playerId)
    {
        if (!team.Players.Contains(playerId))
        {
            team.Players.Add(playerId);
            //TODO: id->플레이어->team->-1
        }
    }


    static void DeletePlayer(this Team team, ref Player player)
    {
        if (team.Players.Contains(player.idNumber))
        {
            team.Players.Remove(player.idNumber);
            player.team = -1;
        }
    }
}
