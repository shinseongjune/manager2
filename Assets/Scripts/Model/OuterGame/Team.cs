using System.Collections.Generic;

public class Team
{
    readonly int idNumber;

    string name;
    int money = 50000;
    int manager = -1;
    readonly List<int> players = new();
    readonly List<int> contracts = new();
    readonly List<int> matches = new();

    public int IDNumber => idNumber;

    public string Name
    {
        get { return name; }
        set { name = value; }
    }
    public int Money
    {
        get { return money; }
        set { money = value; }
    }
    public int Manager
    {
        get { return manager; }
        set { manager = value; }
    }
    public List<int> Players => players;
    public List<int> Contracts => contracts;
    public List<int> Matches => matches;

    public Team(int id, string name)
    {
        idNumber = id;
        this.name = name;
    }

    public Team(int id, string name, int money, int manager = -1)
    {
        idNumber = id;
        this.name = name;
        this.money = money;
        this.manager = manager;
    }
}
