using System;

public static class Maker
{
    static int nextPlayerId = 0;
    static int nextManagerId = 0;
    static int nextTeamId = 0;
    static int nextContractId = 0;

    public static Player MakePlayer(string name)
    {
        Random random = new Random(DateTime.Now.Millisecond + nextPlayerId);
        sbyte age = (sbyte)random.Next(16, 30);
        Player player = new(nextPlayerId++, name, age);
        return player;
    }

    public static Manager MakeManager(string name)
    {
        Manager manager = new(nextManagerId++, name);
        return manager;
    }

    public static Team MakeTeam(string name)
    {
        Team team = new(nextTeamId++, name);
        return team;
    }

    public static Team MakeTeam(string name, int money, int manager = -1)
    {
        Team team = new(nextTeamId++, name, money, manager);
        return team;
    }

    public static Contract MakeContract(int team, int player, Date dDay, int salery)
    {
        int remainingPeriod = dDay.GetHashCode() - GameManager.Instance.NowDate.GetHashCode();
        Contract contract = new(nextContractId, team, player, remainingPeriod, salery);
        return contract;
    }
}
