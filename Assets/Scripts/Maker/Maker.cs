using System;

public static class Maker
{
    public static Player MakePlayer(string name)
    {
        Random random = new(DateTime.Now.Millisecond + GameManager.Instance.NextPlayerId);
        sbyte age = (sbyte)random.Next(16, 30);
        Player player = new(GameManager.Instance.NextPlayerId++, name, age);
        GameManager.Instance.AddPlayer(player);
        return player;
    }

    public static Manager MakeManager(string name)
    {
        Manager manager = new(GameManager.Instance.NextManagerId++, name);
        GameManager.Instance.AddManager(manager);
        return manager;
    }

    public static Team MakeTeam(string name)
    {
        Team team = new(GameManager.Instance.NextTeamId++, name);
        GameManager.Instance.AddTeam(team);
        return team;
    }

    public static Team MakeTeam(string name, int money, int manager = -1)
    {
        Team team = new(GameManager.Instance.NextTeamId++, name, money, manager);
        GameManager.Instance.AddTeam(team);
        return team;
    }

    public static Contract MakeContract(int team, int player, Date dDay, int salery)
    {
        int remainingPeriod = dDay.GetHashCode() - GameManager.Instance.NowDate.GetHashCode();
        Contract contract = new(GameManager.Instance.NextContractId++, team, player, remainingPeriod, salery);
        GameManager.Instance.AddContract(contract);
        return contract;
    }
}
